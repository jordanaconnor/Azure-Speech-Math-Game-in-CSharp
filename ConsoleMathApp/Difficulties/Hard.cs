using System.Diagnostics;

namespace ConsoleMathApp;

public class Hard
{
    public static void HardMode(string mode)
    {
        var isRunning = true;
        Stopwatch timer = new();

        while (isRunning)
        {
            var guessing = true;
            var tryingAgain = false;
            var sessionsFirstQuestion = true;
            var attempts = 1;
            var a = MathLogic.RandomNum(mode);
            var b = MathLogic.RandomNum(mode);

            Console.WriteLine();
            Menu.SubMenu(mode, a, b);

            if (Menu.OperatorSymbol == '/')
            {
                while (a % b != 0)
                {
                    a = MathLogic.RandomNum(mode);
                    b = MathLogic.RandomNum(mode);
                }

                Menu.Answer = MathLogic.Divide(a, b);
            }

            if (Menu.QuittingGame)
            {
                guessing = false;
                isRunning = false;
            }

            //solving the problem
            while (guessing)
            {
                //Displays math problem
                Console.WriteLine();
                Console.WriteLine($"          Problem {Menu.TotalProblemsSolved}");
                Console.WriteLine(" ───────────────────────────");
                Console.WriteLine(" ───────────────────────────");


                if (sessionsFirstQuestion || tryingAgain)
                {
                    MathLogic.PrintProblem(a, b, Menu.OperatorSymbol);
                    timer.Start();
                }
                else
                {
                    a = MathLogic.RandomNum(mode);
                    b = MathLogic.RandomNum(mode);

                    if (Menu.OperatorSymbol == '/')
                    {
                        while (a % b != 0)
                        {
                            a = MathLogic.RandomNum(mode);
                            b = MathLogic.RandomNum(mode);
                        }

                        Menu.Answer = MathLogic.Divide(a, b);
                    }

                    MathLogic.PrintProblem(a, b, Menu.OperatorSymbol);
                    timer.Start();
                }

                if (!sessionsFirstQuestion && Menu.OperatorSymbol == '+')
                    Menu.Answer = MathLogic.Add(a, b);
                else if (!sessionsFirstQuestion && Menu.OperatorSymbol == '-')
                    Menu.Answer = MathLogic.Subtract(a, b);
                else if (!sessionsFirstQuestion && Menu.OperatorSymbol == '*')
                    Menu.Answer = MathLogic.Multiply(a, b);
                else if (!sessionsFirstQuestion && Menu.OperatorSymbol == '/') Menu.Answer = MathLogic.Divide(a, b);

                Console.WriteLine(" ───────────────────────────");
                Console.Write("    Guess: ");

                try
                {
                    var guess = Convert.ToInt32(Console.ReadLine());

                    if (guess == Menu.Answer)
                    {
                        timer.Stop();
                        var timeSpan = timer.Elapsed;
                        timer.Reset();

                        var elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds,
                            timeSpan.Milliseconds / 10);

                        Console.WriteLine(" ───────────────────────────");
                        Console.WriteLine(" You are correct!");
                        Console.WriteLine();
                        Console.WriteLine($" It took you {attempts} attempts.");
                        Console.WriteLine($" Time: {elapsedTime}");
                        tryingAgain = false;

                        // Add to Memory List
                        var completedProblem =
                            Convert.ToString(
                                $"Question {Menu.TotalProblemsSolved}: \n " +
                                $"{a} {Menu.OperatorSymbol} {b} = {Menu.Answer} \n" +
                                $" Attempts: {attempts}\n" +
                                $" Game Mode: {mode}\n" +
                                $" Time: {elapsedTime}");
                        GameHistory.History.Add(completedProblem);
                        Menu.TotalProblemsSolved++;
                        // Play again?
                        Console.WriteLine(" Play again? (Y/N)");
                        Console.WriteLine();
                        Console.WriteLine(" (Y generates a new problem)");
                        Console.WriteLine(" (N returns to the Main Menu)");
                        Console.Write("   Choice: ");
                        var response = Console.ReadLine();

                        if (response.ToUpper() == "N")
                        {
                            Console.Write("Returning to menu");
                            for (var i = 0; i < 3; i++)
                            {
                                Console.Write(" . ");
                                Thread.Sleep(500);
                            }

                            Console.Clear();
                            Menu.MainMenu();
                            guessing = false;
                            isRunning = false;
                        }
                        else if (response.ToUpper() == "Y")
                        {
                            Console.Clear();
                            sessionsFirstQuestion = false;
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
                                sessionsFirstQuestion = false;
                                attempts = 1;
                            }
                        }
                    }
                    else
                    {
                        Console.Write("         Try again...");
                        Thread.Sleep(600);
                        Console.Clear();
                        attempts++;
                        tryingAgain = true;
                    }
                }
                catch
                {
                    Console.Write("         Try again...");
                    Thread.Sleep(600);
                    Console.Clear();
                    attempts++;
                    tryingAgain = true;
                }
            }
        }
    }
}