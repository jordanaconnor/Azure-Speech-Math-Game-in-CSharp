using System.Diagnostics;

namespace ConsoleMathApp;

public class Lightning
{
    public static void LightningMode(string mode)
    {
        var isRunning = true;
        
        Stopwatch timer = new();
        Random rand = new Random();
        
        while (isRunning)
        {
            var guessing = true;
            var attempts = 1;
        
            //solving the problem
            while (guessing)
            {
                var looping = true;
                //Displays math problem

                var randomMode = rand.Next(1, 3);

                switch (randomMode)
                {
                    case 1:
                        mode = "Easy";
                        break;
                    case 2:
                        mode = "Medium";
                        break;
                    case 3:
                        mode = "Hard";
                        break;
                }
                
                var a = MathLogic.RandomNum(mode);
                var b = MathLogic.RandomNum(mode);
                
                var randomOperatorSelector = rand.Next(1, 5); //gen 1-4
                switch (randomOperatorSelector)
                {
                    case 1:
                        Menu.Answer = MathLogic.Add(a, b);
                        Menu.OperatorSymbol = '+';
                        break;
                    case 2:
                        Menu.Answer = MathLogic.Subtract(a, b);
                        Menu.OperatorSymbol = '-';
                        break;
                    case 3:
                        Menu.Answer = MathLogic.Multiply(a, b);
                        Menu.OperatorSymbol = '*';
                        break;
                    case 4:
                        Menu.Answer = MathLogic.Divide(a, b);
                        Menu.OperatorSymbol = '/';
                        break;
                    default:
                        Console.WriteLine("fuck, randomOperatorSelector did something other than 1-4");
                        break;
                }

                if (Menu.OperatorSymbol == '/')
                {
                    while (a % b != 0)
                    {
                        a = MathLogic.RandomNum(mode);
                        b = MathLogic.RandomNum(mode);
                    }
                    Menu.Answer = MathLogic.Divide(a, b);
                }
                timer.Start();
                
                while (looping){
                    Console.WriteLine();
                    Console.WriteLine($"          Problem {Menu.TotalProblemsSolved}");
                    Console.WriteLine(" ───────────────────────────");
                    Console.WriteLine(" ───────────────────────────");
                    MathLogic.PrintProblem(a, b, Menu.OperatorSymbol);
                    

                    Console.WriteLine(" ───────────────────────────");
                    Console.Write("    Guess: ");

                    try 
                    {
                        var guess = Convert.ToInt32(Console.ReadLine());

                        if (guess == Menu.Answer)
                        {
                            // Add to Memory List
                            var completedProblem =
                                Convert.ToString(
                                    $"Question {Menu.TotalProblemsSolved}: \n " +
                                    $"{a} {Menu.OperatorSymbol} {b} = {Menu.Answer} \n" +
                                    $" Attempts: {attempts}\n" +
                                    $" Game Mode: Lightning\n" +
                                    $" Question difficulty: {mode}");
                            GameHistory.History.Add(completedProblem);
                            Menu.TotalProblemsSolved++;

                            Console.WriteLine("          Correct");
                            Console.WriteLine();
                            Console.WriteLine(" Next question? (Y/N)");
                            Console.Write("    Choice: ");
                            var response = Console.ReadLine();

                            if (response.ToUpper() == "N")
                            {
                                Console.Write("Returning to menu");
                                for (int i = 0; i < 3; i++)
                                {
                                    Console.Write(" . ");
                                    Thread.Sleep(500);

                                }

                                Console.Clear();
                                guessing = false;
                                isRunning = false;
                                looping = false;
                                Menu.MainMenu();
                            }
                            else if (response.ToUpper() == "Y")
                            {
                                Console.Clear();
                                looping = false;
                                attempts = 1;
                            }
                            else
                            {
                                while (response.ToUpper() != "Y" && response.ToUpper() != "N")
                                {
                                    Console.WriteLine("Invalid input");
                                    Console.Write("Input: ");
                                    response = Console.ReadLine();
                                    response.ToUpper();
                                }
                                
                                if (response.ToUpper() == "N")
                                {
                                    Console.Clear();
                                    Menu.MainMenu();
                                }

                                if (response.ToUpper() == "Y")
                                {
                                    Console.Clear();
                                    looping = false;
                                    attempts = 1;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("          Try again...");
                            Thread.Sleep(600);
                            Console.Clear();
                            attempts++;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("          Try again...");
                        Thread.Sleep(600);
                        Console.Clear();
                        attempts++;
                    }
                }
            }
        }
    }
}