using TerminalServiceBusExplorer.Compression;

namespace TerminalServiceBusExplorer.Encoding;

public static class Encoder
{
    public static string Decode(byte[] bytes, string encoding)
    {
        if (encoding == "gzip")
        {
            bytes = Gzip.Decompress(bytes);
        }

        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}
