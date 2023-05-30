using System.IO.Compression;

namespace TerminalServiceBusExplorer.Compression;

public static class Gzip
{
    public static byte[] Decompress(byte[] bytes)
    {
        using (var outStream = new MemoryStream())
        {
            using (var inStream = new MemoryStream(bytes))
            using (var gzStream = new GZipStream(inStream, CompressionMode.Decompress))
            {
                gzStream.CopyTo(outStream);
            }

            return outStream.ToArray();
        }
    }
}