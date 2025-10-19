namespace ConsoleMathApp;

public static class GameHistory
{
    public static List<string> History { get; } = new();

    public static async Task ViewHistory()
    {
        if (History.Count == 0) Console.WriteLine("No game history currently.");

        foreach (var problemSolved in History) Console.WriteLine(problemSolved);

        var speech = new Speech();

        Console.WriteLine("──────────────────────────────");
        Console.WriteLine(" Say 'Menu' to return to menu.");
        Console.WriteLine("──────────────────────────────");

        await speech.ReturnFromHistory();
    }
}