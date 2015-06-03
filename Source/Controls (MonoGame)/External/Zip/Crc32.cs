using System.IO;

namespace MonoForce.External.Zip
{
    internal class CRC32
    {
        private const int BUFFER_SIZE = 8192;
        public int TotalBytesRead { get; private set; }
        private readonly uint[] crc32Table;

        public CRC32()
        {
            unchecked
            {
                // This is the official polynomial used by CRC32 in PKZip.
                // Often the polynomial is shown reversed as 0x04C11DB7.
                var dwPolynomial = 0xEDB88320;
                uint i, j;

                crc32Table = new uint[256];

                uint dwCrc;
                for (i = 0; i < 256; i++)
                {
                    dwCrc = i;
                    for (j = 8; j > 0; j--)
                    {
                        if ((dwCrc & 1) == 1)
                        {
                            dwCrc = (dwCrc >> 1) ^ dwPolynomial;
                        }
                        else
                        {
                            dwCrc >>= 1;
                        }
                    }
                    crc32Table[i] = dwCrc;
                }
            }
        }

        public uint GetCrc32(Stream input)
        {
            return GetCrc32AndCopy(input, null);
        }

        public uint GetCrc32AndCopy(Stream input, Stream output)
        {
            unchecked
            {
                uint crc32Result;
                crc32Result = 0xFFFFFFFF;
                var buffer = new byte[BUFFER_SIZE];
                var readSize = BUFFER_SIZE;

                TotalBytesRead = 0;
                var count = input.Read(buffer, 0, readSize);
                if (output != null) output.Write(buffer, 0, count);
                TotalBytesRead += count;
                while (count > 0)
                {
                    for (var i = 0; i < count; i++)
                    {
                        crc32Result = ((crc32Result) >> 8) ^ crc32Table[(buffer[i]) ^ ((crc32Result) & 0x000000FF)];
                    }
                    count = input.Read(buffer, 0, readSize);
                    if (output != null) output.Write(buffer, 0, count);
                    TotalBytesRead += count;
                }

                return ~crc32Result;
            }
        }
    }
}