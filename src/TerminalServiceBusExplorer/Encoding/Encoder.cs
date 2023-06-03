using TerminalServiceBusExplorer.Compression;

namespace TerminalServiceBusExplorer.Encoding;

public static class Encoder
{
    private const string gzipEncoding = "gzip";
    public static string Decode(byte[] bytes, string encoding)
    {
        if (encoding == gzipEncoding)
        {
            bytes = Gzip.Decompress(bytes);
        }

        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}
