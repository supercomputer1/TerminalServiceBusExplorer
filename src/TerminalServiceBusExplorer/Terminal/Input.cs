namespace TerminalServiceBusExplorer.Terminal;

public static class Input
{
    public static int GetChoice(string text)
    {
        Console.WriteLine(text);

        var x = Console.ReadLine();
        return Convert.ToInt32(x);
    }

    public static bool Continue(string text)
    {
        Console.WriteLine(text);

        var x = Console.ReadLine();

        return x.ToUpper() == "Y";
    }
}
