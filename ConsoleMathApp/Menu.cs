using Spectre.Console;

namespace ConsoleMathApp;

public class Menu
{
    public static char OperatorSymbol = '@';
    public static int Answer;
    public static bool QuittingGame;
    public static int TotalProblemsSolved = 1;
    public static int Attempts = 1;


    public static async Task MainMenu()
    {
        var looping = true;
        while (looping)
        {
            Console.WriteLine();
            var table = new Table();
            table.Title("Math Game");
            table.Border(TableBorder.Rounded);
            table.AddColumn("Game Modes");
            table.AddRow("Easy");
            table.AddRow("Medium");
            table.AddRow("Hard");
            table.AddRow("Lightning");
            table.AddRow("History");
            table.AddRow("Quit");

            AnsiConsole.Write(table);

            var mode = "Mode not set";
            string input;
            var speech = new Speech();

            try
            {
                await speech.GetMainMenuSpeechInput();
                input = speech.UserChoice;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            switch (input)
            {
                case "easy":
                    mode = "Easy";
                    await Easy.EasyMode(mode);
                    looping = false;
                    break;
                case "medium":
                    mode = "Medium";
                    await Medium.MediumMode(mode);
                    looping = false;
                    break;
                case "hard":
                    mode = "Hard";
                    await Hard.HardMode(mode);
                    looping = false;
                    break;
                case "lightning":
                    mode = "Lighting";
                    await LightningModeMenu(mode);
                    looping = false;
                    break;
                case "history":
                    looping = false;
                    await GameHistory.ViewHistory();
                    break;
                case "quit":
                    looping = false;
                    PrintQuittingGame();
                    QuittingGame = true;
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
    }

    public static async Task SubMenu(string mode, int a, int b)
    {
        var table = new Table();
        table.Title($"{mode} Mode");
        table.Border(TableBorder.Rounded);
        table.AddColumn("Choose a problem type\n" +
                        "     (Operator)");
        table.AddRow("Add");
        table.AddRow("Subtract");
        table.AddRow("Multiply");
        table.AddRow("Divide");
        table.AddRow("Menu");
        table.AddRow("Quit");

        var looping = true;
        string input;
        var speech = new Speech();

        while (looping)
        {
            AnsiConsole.Write(table);

            try
            {
                await speech.GetSubMenuSpeechInput();
                input = speech.UserChoice;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            switch (input)
            {
                case "add":
                    Console.Clear();
                    OperatorSymbol = '+';
                    Answer = MathLogic.Add(a, b);
                    looping = false;
                    break;
                case "subtract":
                    Console.Clear();
                    OperatorSymbol = '-';
                    Answer = MathLogic.Subtract(a, b);
                    looping = false;
                    break;
                case "multiply":
                    Console.Clear();
                    OperatorSymbol = '*';
                    Answer = MathLogic.Multiply(a, b);
                    looping = false;
                    break;
                case "divide":
                    Console.Clear();
                    OperatorSymbol = '/';
                    Answer = MathLogic.Divide(a, b);
                    looping = false;
                    break;
                case "menu":
                    looping = false;
                    Console.Clear();
                    await MainMenu();
                    break;
                case "quit":
                    looping = false;
                    PrintQuittingGame();
                    QuittingGame = true;
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine();
                    break;
            }
        }
    }

    public static void PrintQuittingGame()
    {
        Console.WriteLine("*************************");
        Console.WriteLine("Quitting game, goodbye!");
        Console.WriteLine("*************************");
    }

    public static async Task LightningModeMenu(string mode)
    {
        var looping = true;
        var table = new Table();
        table.Title($"{mode} Mode");
        table.Border(TableBorder.Rounded);
        table.AddColumn("Menu");
        table.AddRow("Begin");
        table.AddRow("Menu");
        table.AddRow("Quit           ");

        while (looping)
        {
            AnsiConsole.Write(table);
            Console.WriteLine(" An infinite and random\n" +
                              " challenge mode from any\n" +
                              "       difficulty.\n");
            Console.Write(" Menu selection: ");

            var speech = new Speech();
            await speech.LightningModeMenuSelection();
            var input = Speech.LightningModeSelection;
            switch (input)
            {
                case "begin":
                    looping = false;
                    Console.Clear();
                    await Lightning.LightningMode(mode);
                    break;
                case "menu":
                    looping = false;
                    Console.Clear();
                    await MainMenu();
                    break;
                case "quit":
                    looping = false;
                    Console.Clear();
                    PrintQuittingGame();
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
    }
}