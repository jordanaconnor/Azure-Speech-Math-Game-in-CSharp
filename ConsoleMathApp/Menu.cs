namespace ConsoleMathApp;
using Spectre.Console;

public class Menu
{
    public static char OperatorSymbol = '@';
    public static int Answer = 0;
    public static bool QuittingGame = false;
    public static int TotalProblemsSolved = 1;
    public static void MainMenu()
    {
        var looping = true;
        while (looping)
        {
            int input = 1;
            
            Console.WriteLine();
            var table = new Table();
            table.Title("Math Game");
            table.Border(TableBorder.Rounded);
            table.AddColumn("#");
            table.Columns[0].Centered();
            table.AddColumn("Options");
            table.AddRow("1.", "Easy");
            table.AddRow("2.", "Medium");
            table.AddRow("3.", "Hard");
            table.AddRow("4.", "Lightning Round");
            table.AddRow("5.", "Game History");
            table.AddRow("6.", "Quit");

            AnsiConsole.Write(table);

            var mode = "Mode not set";

            try
            {
                Console.Write("Input: ");
                input = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.Clear();
                MainMenu();
            }

            switch (input)
            {
                case 1:
                    mode = "Easy";
                    Easy.EasyMode(mode);
                    looping = false;
                    break;
                case 2:
                    mode = "Medium";
                    Medium.MediumMode(mode);
                    looping = false;
                    break;
                case 3:
                    mode = "Hard";
                    Hard.HardMode(mode);
                    looping = false;
                    break;
                case 4:
                    //TODO randomize the lightning mode setting
                    mode = "Lighting";
                    LightningModeMenu(mode);
                    looping = false;
                    break;
                case 5:
                    looping = false;
                    GameHistory.ViewHistory();
                    break;
                case 6:
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
    
    public static void SubMenu(string mode, int a, int b)
    {
        var table = new Table();
        table.Title($"{mode} Mode");
        table.Border(TableBorder.Rounded);
        table.AddColumn("#");
        table.AddColumn("Problem Type");
        table.AddRow("1.", "+ ");
        table.AddRow("2.", "- ");
        table.AddRow("3.", "* ");
        table.AddRow("4.", "/ ");
        table.AddRow("5.", "Menu");
        table.AddRow("6.", "Quit           ");

        var looping = true;
        
        while (looping){
            AnsiConsole.Write(table);
            Console.WriteLine("Select a math operator ");
            Console.Write("Input: ");

            try
            {
                //User selection for game
                var userOperatorInput = Convert.ToInt32(Console.ReadLine());

                //Depending on the number chosen from the menu, the switch statements runs the math for the game.
                switch (userOperatorInput)
                {
                    case 1:
                        Console.Clear();
                        OperatorSymbol = '+';
                        Answer = MathLogic.Add(a, b);
                        looping = false;
                        break;
                    case 2:
                        Console.Clear();
                        OperatorSymbol = '-';
                        Answer = MathLogic.Subtract(a, b);
                        looping = false;
                        break;
                    case 3:
                        Console.Clear();
                        OperatorSymbol = '*';
                        Answer = MathLogic.Multiply(a, b);
                        looping = false;
                        break;
                    case 4:
                        Console.Clear();
                        OperatorSymbol = '/';
                        Answer = MathLogic.Divide(a, b);
                        looping = false;
                        break;
                    case 5:
                        looping = false;
                        Console.Clear();
                        MainMenu();
                        break;
                    case 6:
                        looping = false;
                        Console.Clear();
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
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine();
            }
        }
    }

    public static void ReturnToMenu()
    {
        Console.WriteLine("Return to menu?");
        Console.WriteLine("Y to return");
        Console.WriteLine("N to quit game.");
    }

    public static void PrintQuittingGame()
    {
        Console.WriteLine("*************************");
        Console.WriteLine("Quitting game, goodbye!");
        Console.WriteLine("*************************");
    }

    public static void LightningModeMenu(string mode)
    {
        var looping = true;
        var table = new Table();
        table.Title($"{mode} Mode");
        table.Border(TableBorder.Rounded);
        table.AddColumn("#");
        table.AddColumn("Menu");
        table.AddRow("1.", "Begin");
        table.AddRow("2.", "Menu");
        table.AddRow("3.", "Quit           ");

        while (looping)
        {
            AnsiConsole.Write(table);
            Console.WriteLine(); //                    l
            Console.WriteLine("An infinite and random\n" +
                              "challenge mode from any\n" +
                              "difficulty.");
            Console.Write("Menu selection: ");

            var input = 0;
            try
            {
                input = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.Clear();
                LightningModeMenu(mode);
            }

            switch (input)
            {
                case 1:
                    looping = false;
                    Console.Clear();
                    Lightning.LightningMode(mode);
                    break;
                case 2:
                    looping = false;
                    Console.Clear();
                    MainMenu();
                    break;
                case 3:
                    looping = false;
                    Console.Clear();
                    PrintQuittingGame();
                    QuittingGame = true;
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }


    }
    
}