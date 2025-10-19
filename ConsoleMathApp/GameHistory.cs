namespace ConsoleMathApp;

public static class GameHistory
{
    public static List<string> History { get; } = new();

    public static void ViewHistory()
    {
        var looping = true;
        Console.Clear();

        if (History.Count == 0) Console.WriteLine("No game history currently.");

        foreach (var problemSolved in History) Console.WriteLine(problemSolved);

        while (looping)
        {
            var input = "whatever I want this to be as long as it doesnt start with y or Y";
            Console.Write("Enter Q to return to Menu: ");
            input = Console.ReadLine();
            if (input.ToUpper() == "Q")
            {
                looping = false;
                Console.Clear();
                Menu.MainMenu();
            }
            else
            {
                Console.Clear();
                ViewHistory();
            }
        }
    }
}