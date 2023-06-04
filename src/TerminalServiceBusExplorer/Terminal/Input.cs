namespace TerminalServiceBusExplorer.Terminal;

public static class Input
{

    private static readonly List<string> validInputs = new List<string>() { "Y", "N", "y", "n" };

    public static int GetChoice(string text)
    {
        Console.WriteLine(text);

        var choice = Console.ReadLine();
        if (choice == null)
        {
            throw new NullReferenceException(nameof(choice));
        }

        if (Int32.TryParse(choice, out int value))
        {
            if (value > Int32.MaxValue)
            {
                throw new OverflowException(nameof(value));
            }

            return value;
        }

        throw new Exception($"{choice} is not a valid integer.");

    }

    public static bool Continue(string text)
    {
        Console.WriteLine(text);

        string? choice = null;


        while (!validInputs.Contains(choice))
        {
            choice = Console.ReadLine();

            Console.WriteLine("y/n");
        }


        if (choice == null)
        {
            throw new NullReferenceException(nameof(choice));
        }


        return choice.ToUpper() == "Y";
    }
}
