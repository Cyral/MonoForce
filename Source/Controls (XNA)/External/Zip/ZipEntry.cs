using System;
using System.IO;
using System.IO.Compression;

namespace TomShane.Neoforce.External.Zip
{
    internal class ZipEntry
    {
        private const int ZipEntryDataDescriptorSignature = 0x08074b50;
        private const int ZipEntrySignature = 0x04034b50;
        public short BitField { get; private set; }
        public int CompressedSize { get; private set; }
        public short CompressionMethod { get; private set; }

        public double CompressionRatio
        {
            get { return 100 * (1.0 - (1.0 * CompressedSize) / (1.0 * UncompressedSize)); }
        }

        public string FileName { get; private set; }
        public DateTime LastModified { get; private set; }
        // when this is set, we trim the volume (eg C:\) off any fully-qualified pathname, 
        // before writing the ZipEntry into the ZipFile. 
        public bool TrimVolumeFromFullyQualifiedPaths { get; set; } = true;
        public int UncompressedSize { get; private set; }
        public short VersionNeeded { get; private set; }
        internal byte[] Header { get; private set; }

        private byte[] _FileData
        {
            get
            {
                if (__filedata == null)
                {
                }
                return __filedata;
            }
        }

        private DeflateStream CompressedStream
        {
            get
            {
                if (_CompressedStream == null)
                {
                    _UnderlyingMemoryStream = new MemoryStream();
                    var LeaveUnderlyingStreamOpen = true;
                    _CompressedStream = new DeflateStream(_UnderlyingMemoryStream,
                        CompressionMode.Compress,
                        LeaveUnderlyingStreamOpen);
                }
                return _CompressedStream;
            }
        }

        private byte[] __filedata;
        private DeflateStream _CompressedStream;
        private int _Crc32;
        private bool _Debug;
        private byte[] _Extra;
        private int _LastModDateTime;
        private int _RelativeOffsetOfHeader;
        private MemoryStream _UnderlyingMemoryStream;

        public static ZipEntry Read(Stream s)
        {
            return Read(s, false);
        }

        public void Extract()
        {
            Extract(".");
        }

        public void Extract(Stream s)
        {
            Extract(null, s);
        }

        public void Extract(string basedir)
        {
            Extract(basedir, null);
        }

        internal static ZipEntry Create(string filename)
        {
            var entry = new ZipEntry();
            entry.FileName = filename;

            entry.LastModified = File.GetLastWriteTime(filename);
            // adjust the time if the .NET BCL thinks it is in DST.  
            // see the note elsewhere in this file for more info. 
            if (entry.LastModified.IsDaylightSavingTime())
            {
                var AdjustedTime = entry.LastModified - new TimeSpan(1, 0, 0);
                entry._LastModDateTime = Shared.DateTimeToPacked(AdjustedTime);
            }
            else
                entry._LastModDateTime = Shared.DateTimeToPacked(entry.LastModified);

            // we don't actually slurp in the file until the caller invokes Write on this entry.

            return entry;
        }

        internal static ZipEntry Read(Stream s, bool TurnOnDebug)
        {
            var entry = new ZipEntry();
            entry._Debug = TurnOnDebug;
            if (!ReadHeader(s, entry)) return null;

            entry.__filedata = new byte[entry.CompressedSize];
            var n = s.Read(entry._FileData, 0, entry._FileData.Length);
            if (n != entry._FileData.Length)
            {
                throw new Exception("badly formatted zip file.");
            }
            // finally, seek past the (already read) Data descriptor if necessary
            if ((entry.BitField & 0x0008) == 0x0008)
            {
                s.Seek(16, SeekOrigin.Current);
            }
            return entry;
        }

        internal Stream GetStream()
        {
            var memstream = new MemoryStream(_FileData);

            if (CompressedSize == UncompressedSize)
                return memstream;

            return new DeflateStream(
                memstream, CompressionMode.Decompress);
        }

        internal void Write(Stream s)
        {
            var bytes = new byte[4096];
            int n;

            // write the header:
            WriteHeader(s, bytes);

            // write the actual file data: 
            _UnderlyingMemoryStream.Position = 0;

            if (_Debug)
            {
                Console.WriteLine("{0}: writing compressed data to zipfile...", FileName);
                Console.WriteLine("{0}: total data length: {1}", FileName, _UnderlyingMemoryStream.Length);
            }
            while ((n = _UnderlyingMemoryStream.Read(bytes, 0, bytes.Length)) != 0)
            {
                if (_Debug)
                {
                    Console.WriteLine("{0}: transferring {1} bytes...", FileName, n);

                    for (var j = 0; j < n; j += 2)
                    {
                        if ((j > 0) && (j % 40 == 0))
                            Console.WriteLine();
                        Console.Write(" {0:X2}", bytes[j]);
                        if (j + 1 < n)
                            Console.Write("{0:X2}", bytes[j + 1]);
                    }
                    Console.WriteLine("\n");
                }

                s.Write(bytes, 0, n);
            }

            //_CompressedStream.Close();
            //_CompressedStream= null;
            _UnderlyingMemoryStream.Close();
            _UnderlyingMemoryStream = null;
        }

        internal void WriteCentralDirectoryEntry(Stream s)
        {
            var bytes = new byte[4096];
            var i = 0;
            // signature
            bytes[i++] = ZipDirEntry.ZipDirEntrySignature & 0x000000FF;
            bytes[i++] = (ZipDirEntry.ZipDirEntrySignature & 0x0000FF00) >> 8;
            bytes[i++] = (ZipDirEntry.ZipDirEntrySignature & 0x00FF0000) >> 16;
            bytes[i++] = (byte)((ZipDirEntry.ZipDirEntrySignature & 0xFF000000) >> 24);

            // Version Made By
            bytes[i++] = Header[4];
            bytes[i++] = Header[5];

            // Version Needed, Bitfield, compression method, lastmod,
            // crc, sizes, filename length and extra field length -
            // are all the same as the local file header. So just copy them
            var j = 0;
            for (j = 0; j < 26; j++)
                bytes[i + j] = Header[4 + j];

            i += j; // positioned at next available byte

            // File Comment Length
            bytes[i++] = 0;
            bytes[i++] = 0;

            // Disk number start
            bytes[i++] = 0;
            bytes[i++] = 0;

            // internal file attrs            
            bytes[i++] = 1;
            bytes[i++] = 0;

            // external file attrs            
            bytes[i++] = 0x20;
            bytes[i++] = 0;
            bytes[i++] = 0xb6;
            bytes[i++] = 0x81;

            // relative offset of local header (I think this can be zero)
            bytes[i++] = (byte)(_RelativeOffsetOfHeader & 0x000000FF);
            bytes[i++] = (byte)((_RelativeOffsetOfHeader & 0x0000FF00) >> 8);
            bytes[i++] = (byte)((_RelativeOffsetOfHeader & 0x00FF0000) >> 16);
            bytes[i++] = (byte)((_RelativeOffsetOfHeader & 0xFF000000) >> 24);

            if (_Debug) Console.WriteLine("\ninserting filename into CDS: (length= {0})", Header.Length - 30);
            // actual filename (starts at offset 34 in header) 
            for (j = 0; j < Header.Length - 30; j++)
            {
                bytes[i + j] = Header[30 + j];
                if (_Debug) Console.Write(" {0:X2}", bytes[i + j]);
            }
            if (_Debug) Console.WriteLine();
            i += j;

            s.Write(bytes, 0, i);
        }

        private static bool ReadHeader(Stream s, ZipEntry ze)
        {
            var signature = Shared.ReadSignature(s);

            // return null if this is not a local file header signature
            if (SignatureIsNotValid(signature))
            {
                s.Seek(-4, SeekOrigin.Current);
                if (ze._Debug)
                    Console.WriteLine("  ZipEntry::Read(): Bad signature ({0:X8}) at position {1}", signature,
                        s.Position);
                return false;
            }

            var block = new byte[26];
            var n = s.Read(block, 0, block.Length);
            if (n != block.Length) return false;

            var i = 0;
            ze.VersionNeeded = (short)(block[i++] + block[i++] * 256);
            ze.BitField = (short)(block[i++] + block[i++] * 256);
            ze.CompressionMethod = (short)(block[i++] + block[i++] * 256);
            ze._LastModDateTime = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;

            // the PKZIP spec says that if bit 3 is set (0x0008), then the CRC, Compressed size, and uncompressed size
            // come directly after the file data.  The only way to find it is to scan the zip archive for the signature of 
            // the Data Descriptor, and presume that that signature does not appear in the (compressed) data of the compressed file.  

            if ((ze.BitField & 0x0008) != 0x0008)
            {
                ze._Crc32 = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;
                ze.CompressedSize = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 +
                                    block[i++] * 256 * 256 * 256;
                ze.UncompressedSize = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 +
                                      block[i++] * 256 * 256 * 256;
            }
            else
            {
                // the CRC, compressed size, and uncompressed size are stored later in the stream.
                // here, we advance the pointer.
                i += 12;
            }

            var filenameLength = (short)(block[i++] + block[i++] * 256);
            var extraFieldLength = (short)(block[i++] + block[i++] * 256);

            block = new byte[filenameLength];
            n = s.Read(block, 0, block.Length);
            ze.FileName = Shared.StringFromBuffer(block, 0, block.Length);

            ze._Extra = new byte[extraFieldLength];
            n = s.Read(ze._Extra, 0, ze._Extra.Length);

            // transform the time data into something usable
            ze.LastModified = Shared.PackedToDateTime(ze._LastModDateTime);

            // actually get the compressed size and CRC if necessary
            if ((ze.BitField & 0x0008) == 0x0008)
            {
                var posn = s.Position;
                var SizeOfDataRead = Shared.FindSignature(s, ZipEntryDataDescriptorSignature);
                if (SizeOfDataRead == -1) return false;

                // read 3x 4-byte fields (CRC, Compressed Size, Uncompressed Size)
                block = new byte[12];
                n = s.Read(block, 0, block.Length);
                if (n != 12) return false;
                i = 0;
                ze._Crc32 = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;
                ze.CompressedSize = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 +
                                    block[i++] * 256 * 256 * 256;
                ze.UncompressedSize = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 +
                                      block[i++] * 256 * 256 * 256;

                if (SizeOfDataRead != ze.CompressedSize)
                    throw new Exception("Data format error (bit 3 is set)");

                // seek back to previous position, to read file data
                s.Seek(posn, SeekOrigin.Begin);
            }

            return true;
        }

        private static bool SignatureIsNotValid(int signature)
        {
            return (signature != ZipEntrySignature);
        }

        // pass in either basedir or s, but not both. 
        // In other words, you can extract to a stream or to a directory, but not both!
        private void Extract(string basedir, Stream s)
        {
            string TargetFile = null;
            if (basedir != null)
            {
                TargetFile = Path.Combine(basedir, FileName);

                // check if a directory
                if (FileName.EndsWith("/"))
                {
                    if (!Directory.Exists(TargetFile))
                        Directory.CreateDirectory(TargetFile);
                    return;
                }
            }
            else if (s != null)
            {
                if (FileName.EndsWith("/"))
                    // extract a directory to streamwriter?  nothing to do!
                    return;
            }
            else throw new Exception("Invalid input.");


            using (var memstream = new MemoryStream(_FileData))
            {
                Stream input = null;
                try
                {
                    if (CompressedSize == UncompressedSize)
                    {
                        // the System.IO.Compression.DeflateStream class does not handle uncompressed data.
                        // so if an entry is not compressed, then we just translate the bytes directly.
                        input = memstream;
                    }
                    else
                    {
                        input = new DeflateStream(memstream, CompressionMode.Decompress);
                    }


                    if (TargetFile != null)
                    {
                        // ensure the target path exists
                        if (!Directory.Exists(Path.GetDirectoryName(TargetFile)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(TargetFile));
                        }
                    }


                    Stream output = null;
                    try
                    {
                        if (TargetFile != null)
                            output = new FileStream(TargetFile, FileMode.CreateNew);
                        else
                            output = s;


                        var bytes = new byte[4096];
                        int n;

                        if (_Debug)
                        {
                            Console.WriteLine("{0}: _FileData.Length= {1}", TargetFile, _FileData.Length);
                            Console.WriteLine("{0}: memstream.Position: {1}", TargetFile, memstream.Position);
                            n = _FileData.Length;
                            if (n > 1000)
                            {
                                n = 500;
                                Console.WriteLine("{0}: truncating dump from {1} to {2} bytes...", TargetFile,
                                    _FileData.Length, n);
                            }
                            for (var j = 0; j < n; j += 2)
                            {
                                if ((j > 0) && (j % 40 == 0))
                                    Console.WriteLine();
                                Console.Write(" {0:X2}", _FileData[j]);
                                if (j + 1 < n)
                                    Console.Write("{0:X2}", _FileData[j + 1]);
                            }
                            Console.WriteLine("\n");
                        }

                        n = 1; // anything non-zero
                        while (n != 0)
                        {
                            if (_Debug) Console.WriteLine("{0}: about to read...", TargetFile);
                            n = input.Read(bytes, 0, bytes.Length);
                            if (_Debug) Console.WriteLine("{0}: got {1} bytes", TargetFile, n);
                            if (n > 0)
                            {
                                if (_Debug) Console.WriteLine("{0}: about to write...", TargetFile);
                                output.Write(bytes, 0, n);
                            }
                        }
                    }
                    finally
                    {
                        // we only close the output stream if we opened it. 
                        if ((output != null) && (TargetFile != null))
                        {
                            output.Close();
                            output.Dispose();
                        }
                    }

                    if (TargetFile != null)
                    {
                        // We may have to adjust the last modified time to compensate
                        // for differences in how the .NET Base Class Library deals
                        // with daylight saving time (DST) versus how the Windows
                        // filesystem deals with daylight saving time. See 
                        // http://blogs.msdn.com/oldnewthing/archive/2003/10/24/55413.aspx for some context. 

                        // in a nutshell: Daylight savings time rules change regularly.  In
                        // 2007, for example, the inception week of DST changed.  In 1977,
                        // DST was in place all year round. in 1945, likewise.  And so on.
                        // Win32 does not attempt to guess which time zone rules were in
                        // effect at the time in question.  It will render a time as
                        // "standard time" and allow the app to change to DST as necessary.
                        //  .NET makes a different choice.

                        // -------------------------------------------------------
                        // Compare the output of FileInfo.LastWriteTime.ToString("f") with
                        // what you see in the property sheet for a file that was last
                        // written to on the other side of the DST transition. For example,
                        // suppose the file was last modified on October 17, during DST but
                        // DST is not currently in effect. Explorer's file properties
                        // reports Thursday, October 17, 2003, 8:45:38 AM, but .NETs
                        // FileInfo reports Thursday, October 17, 2003, 9:45 AM.

                        // Win32 says, "Thursday, October 17, 2002 8:45:38 AM PST". Note:
                        // Pacific STANDARD Time. Even though October 17 of that year
                        // occurred during Pacific Daylight Time, Win32 displays the time as
                        // standard time because that's what time it is NOW.

                        // .NET BCL assumes that the current DST rules were in place at the
                        // time in question.  So, .NET says, "Well, if the rules in effect
                        // now were also in effect on October 17, 2003, then that would be
                        // daylight time" so it displays "Thursday, October 17, 2003, 9:45
                        // AM PDT" - daylight time.

                        // So .NET gives a value which is more intuitively correct, but is
                        // also potentially incorrect, and which is not invertible. Win32
                        // gives a value which is intuitively incorrect, but is strictly
                        // correct.
                        // -------------------------------------------------------

                        // With this adjustment, I add one hour to the tweaked .NET time, if
                        // necessary.  That is to say, if the time in question had occurred
                        // in what the .NET BCL assumed to be DST (an assumption that may be
                        // wrong given the constantly changing DST rules).

#if !XBOX
                        if (LastModified.IsDaylightSavingTime())
                        {
                            var AdjustedLastModified = LastModified + new TimeSpan(1, 0, 0);
                            File.SetLastWriteTime(TargetFile, AdjustedLastModified);
                        }
                        else
                            File.SetLastWriteTime(TargetFile, LastModified);
#endif
                    }
                }
                finally
                {
                    // we only close the output stream if we opened it. 
                    // we cannot use using() here because in some cases we do not want to Dispose the stream!
                    if ((input != null) && (input != memstream))
                    {
                        input.Close();
                        input.Dispose();
                    }
                }
            }
        }

        private void WriteHeader(Stream s, byte[] bytes)
        {
            // write the header info

            var i = 0;
            // signature
            bytes[i++] = ZipEntrySignature & 0x000000FF;
            bytes[i++] = (ZipEntrySignature & 0x0000FF00) >> 8;
            bytes[i++] = (ZipEntrySignature & 0x00FF0000) >> 16;
            bytes[i++] = (byte)((ZipEntrySignature & 0xFF000000) >> 24);

            // version needed
            short FixedVersionNeeded = 0x14; // from examining existing zip files
            bytes[i++] = (byte)(FixedVersionNeeded & 0x00FF);
            bytes[i++] = (byte)((FixedVersionNeeded & 0xFF00) >> 8);

            // bitfield
            short BitField = 0x00; // from examining existing zip files
            bytes[i++] = (byte)(BitField & 0x00FF);
            bytes[i++] = (byte)((BitField & 0xFF00) >> 8);

            // compression method
            short CompressionMethod = 0x08; // 0x08 = Deflate
            bytes[i++] = (byte)(CompressionMethod & 0x00FF);
            bytes[i++] = (byte)((CompressionMethod & 0xFF00) >> 8);

            // LastMod
            bytes[i++] = (byte)(_LastModDateTime & 0x000000FF);
            bytes[i++] = (byte)((_LastModDateTime & 0x0000FF00) >> 8);
            bytes[i++] = (byte)((_LastModDateTime & 0x00FF0000) >> 16);
            bytes[i++] = (byte)((_LastModDateTime & 0xFF000000) >> 24);

            // CRC32 (Int32)
            var crc32 = new CRC32();
            uint crc = 0;
            using (Stream input = File.OpenRead(FileName))
            {
                crc = crc32.GetCrc32AndCopy(input, CompressedStream);
            }
            CompressedStream.Close(); // to get the footer bytes written to the underlying stream

            bytes[i++] = (byte)(crc & 0x000000FF);
            bytes[i++] = (byte)((crc & 0x0000FF00) >> 8);
            bytes[i++] = (byte)((crc & 0x00FF0000) >> 16);
            bytes[i++] = (byte)((crc & 0xFF000000) >> 24);

            // CompressedSize (Int32)
            var isz = (int)_UnderlyingMemoryStream.Length;
            var sz = (uint)isz;
            bytes[i++] = (byte)(sz & 0x000000FF);
            bytes[i++] = (byte)((sz & 0x0000FF00) >> 8);
            bytes[i++] = (byte)((sz & 0x00FF0000) >> 16);
            bytes[i++] = (byte)((sz & 0xFF000000) >> 24);

            // UncompressedSize (Int32)
            if (_Debug) Console.WriteLine("Uncompressed Size: {0}", crc32.TotalBytesRead);
            bytes[i++] = (byte)(crc32.TotalBytesRead & 0x000000FF);
            bytes[i++] = (byte)((crc32.TotalBytesRead & 0x0000FF00) >> 8);
            bytes[i++] = (byte)((crc32.TotalBytesRead & 0x00FF0000) >> 16);
            bytes[i++] = (byte)((crc32.TotalBytesRead & 0xFF000000) >> 24);

            // filename length (Int16)
            var length = (short)FileName.Length;
            // see note below about TrimVolumeFromFullyQualifiedPaths.
            if ((TrimVolumeFromFullyQualifiedPaths) && (FileName[1] == ':') && (FileName[2] == '\\')) length -= 3;
            bytes[i++] = (byte)(length & 0x00FF);
            bytes[i++] = (byte)((length & 0xFF00) >> 8);

            // extra field length (short)
            short ExtraFieldLength = 0x00;
            bytes[i++] = (byte)(ExtraFieldLength & 0x00FF);
            bytes[i++] = (byte)((ExtraFieldLength & 0xFF00) >> 8);

            // Tue, 27 Mar 2007  16:35

            // Creating a zip that contains entries with "fully qualified" pathnames
            // can result in a zip archive that is unreadable by Windows Explorer.
            // Such archives are valid according to other tools but not to explorer.
            // To avoid this, we can trim off the leading volume name and slash (eg
            // c:\) when creating (writing) a zip file.  We do this by default and we
            // leave the old behavior available with the
            // TrimVolumeFromFullyQualifiedPaths flag - set it to false to get the old
            // behavior.  It only affects zip creation.

            // actual filename
            var c = ((TrimVolumeFromFullyQualifiedPaths) && (FileName[1] == ':') && (FileName[2] == '\\'))
                ? FileName.Substring(3).ToCharArray()
                : // trim off volume letter, colon, and slash
                FileName.ToCharArray();
            var j = 0;

            if (_Debug)
            {
                Console.WriteLine("local header: writing filename, {0} chars", c.Length);
                Console.WriteLine("starting offset={0}", i);
            }
            for (j = 0; (j < c.Length) && (i + j < bytes.Length); j++)
            {
                bytes[i + j] = BitConverter.GetBytes(c[j])[0];
                if (_Debug) Console.Write(" {0:X2}", bytes[i + j]);
            }
            if (_Debug) Console.WriteLine();

            i += j;

            // extra field (we always write nothing in this implementation)
            // ;;

            // remember the file offset of this header
            _RelativeOffsetOfHeader = (int)s.Length;


            if (_Debug)
            {
                Console.WriteLine("\nAll header data:");
                for (j = 0; j < i; j++)
                    Console.Write(" {0:X2}", bytes[j]);
                Console.WriteLine();
            }
            // finally, write the header to the stream
            s.Write(bytes, 0, i);

            // preserve this header data for use with the central directory structure.
            Header = new byte[i];
            if (_Debug) Console.WriteLine("preserving header of {0} bytes", Header.Length);
            for (j = 0; j < i; j++)
                Header[j] = bytes[j];
        }
    }
}