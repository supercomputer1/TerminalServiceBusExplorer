namespace TerminalServiceBusExplorer.Extensions;

public static class ReadOnlyDictionaryExtensions
{
    public static T GetValue<T>(this IReadOnlyDictionary<string, object?> input, string key)
    {
        if (input.TryGetValue(key, out object? value))
        {
            if (value is null) throw new NullReferenceException();

            return (T)value;
        }

        throw new Exception($"Key {key} not present in dictionary.");
    }
}
