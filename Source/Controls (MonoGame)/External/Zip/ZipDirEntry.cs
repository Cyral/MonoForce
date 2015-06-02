using System;
using System.IO;

namespace TomShane.Neoforce.External.Zip
{
    internal class ZipDirEntry
    {
        internal const int ZipDirEntrySignature = 0x02014b50;
        public string Comment { get; private set; }
        public int CompressedSize { get; private set; }
        public short CompressionMethod { get; private set; }

        public double CompressionRatio
        {
            get { return 100 * (1.0 - (1.0 * CompressedSize) / (1.0 * UncompressedSize)); }
        }

        public string FileName { get; private set; }
        public DateTime LastModified { get; private set; }
        public int UncompressedSize { get; private set; }
        public short VersionMadeBy { get; private set; }
        public short VersionNeeded { get; private set; }
        private short _BitField;
        private int _Crc32;
        private bool _Debug;
        private byte[] _Extra;
        private int _LastModDateTime;

        internal ZipDirEntry(ZipEntry ze)
        {
        }

        private ZipDirEntry()
        {
        }

        internal static ZipDirEntry Read(Stream s)
        {
            return Read(s, false);
        }

        internal static ZipDirEntry Read(Stream s, bool TurnOnDebug)
        {
            var signature = Shared.ReadSignature(s);
            // return null if this is not a local file header signature
            if (SignatureIsNotValid(signature))
            {
                s.Seek(-4, SeekOrigin.Current);
                if (TurnOnDebug)
                    Console.WriteLine("  ZipDirEntry::Read(): Bad signature ({0:X8}) at position {1}", signature,
                        s.Position);
                return null;
            }

            var block = new byte[42];
            var n = s.Read(block, 0, block.Length);
            if (n != block.Length) return null;

            var i = 0;
            var zde = new ZipDirEntry();

            zde._Debug = TurnOnDebug;
            zde.VersionMadeBy = (short)(block[i++] + block[i++] * 256);
            zde.VersionNeeded = (short)(block[i++] + block[i++] * 256);
            zde._BitField = (short)(block[i++] + block[i++] * 256);
            zde.CompressionMethod = (short)(block[i++] + block[i++] * 256);
            zde._LastModDateTime = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;
            zde._Crc32 = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;
            zde.CompressedSize = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;
            zde.UncompressedSize = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;

            zde.LastModified = Shared.PackedToDateTime(zde._LastModDateTime);

            var filenameLength = (short)(block[i++] + block[i++] * 256);
            var extraFieldLength = (short)(block[i++] + block[i++] * 256);
            var commentLength = (short)(block[i++] + block[i++] * 256);
            var diskNumber = (short)(block[i++] + block[i++] * 256);
            var internalFileAttrs = (short)(block[i++] + block[i++] * 256);
            var externalFileAttrs = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 +
                                    block[i++] * 256 * 256 * 256;
            var Offset = block[i++] + block[i++] * 256 + block[i++] * 256 * 256 + block[i++] * 256 * 256 * 256;

            block = new byte[filenameLength];
            n = s.Read(block, 0, block.Length);
            zde.FileName = Shared.StringFromBuffer(block, 0, block.Length);

            zde._Extra = new byte[extraFieldLength];
            n = s.Read(zde._Extra, 0, zde._Extra.Length);

            block = new byte[commentLength];
            n = s.Read(block, 0, block.Length);
            zde.Comment = Shared.StringFromBuffer(block, 0, block.Length);

            return zde;
        }

        private static bool SignatureIsNotValid(int signature)
        {
            return (signature != ZipDirEntrySignature);
        }
    }
}