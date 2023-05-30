namespace TerminalServiceBusExplorer.Extensions.cs;

public static class DictionaryExtensions
{
    public static void Print(this Dictionary<int, string> input)
    {
        foreach (var keyValuePair in input)
        {
            Console.WriteLine($"Key: {keyValuePair.Key} | Name: {keyValuePair.Value}");
        }
    }
}
