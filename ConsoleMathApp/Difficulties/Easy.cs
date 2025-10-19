using System.Diagnostics;

namespace ConsoleMathApp;

public class Easy
{
    public static async Task EasyMode(string mode)
    {
        var isRunning = true;
        Stopwatch timer = new();
        var speech = new Speech();
        Menu.Attempts = 1;

        while (isRunning)
        {
            var guessing = true;
            var tryingAgain = false;
            var sessionsFirstQuestion = true;
            var a = MathLogic.RandomNum(mode);
            var b = MathLogic.RandomNum(mode);

            Console.WriteLine();
            await Menu.SubMenu(mode, a, b);

            if (Menu.OperatorSymbol == '/')
            {
                while (a % b != 0)
                {
                    a = MathLogic.RandomNum(mode);
                    b = MathLogic.RandomNum(mode);
                }

                Menu.Answer = MathLogic.Divide(a, b);
            }

            if (Menu.OperatorSymbol == '-')
            {
                while (a < b)
                {
                    a = MathLogic.RandomNum(mode);
                    b = MathLogic.RandomNum(mode);
                }

                Menu.Answer = MathLogic.Subtract(a, b);
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

                    if (Menu.OperatorSymbol == '-')
                    {
                        while (a < b)
                        {
                            a = MathLogic.RandomNum(mode);
                            b = MathLogic.RandomNum(mode);
                        }

                        Menu.Answer = MathLogic.Subtract(a, b);
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
                else if (!sessionsFirstQuestion && Menu.OperatorSymbol == '/')
                    Menu.Answer = MathLogic.Divide(a, b);

                Console.WriteLine(" ───────────────────────────");
                Console.WriteLine($"Answer: {Menu.Answer}");
                Console.Write("    Guess: ");

                try
                {
                    await speech.GetGuessSpeechInput();
                    var guess = Speech.NumGuess;

                    if (guess == Menu.Answer)
                    {
                        speech.ContinueGettingInput = false;
                        timer.Stop();
                        var timeSpan = timer.Elapsed;
                        timer.Reset();

                        var elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds,
                            timeSpan.Milliseconds / 10);

                        Console.WriteLine(" ───────────────────────────");
                        Console.WriteLine(" You are correct!");
                        Console.WriteLine();
                        Console.WriteLine($" It took you {Menu.Attempts} attempts.");
                        Console.WriteLine($" Time: {elapsedTime}");
                        tryingAgain = false;

                        // Add to Memory List
                        var completedProblem =
                            Convert.ToString(
                                $"Question {Menu.TotalProblemsSolved}: \n " +
                                $"{a} {Menu.OperatorSymbol} {b} = {Menu.Answer} \n" +
                                $" Attempts: {Menu.Attempts}\n" +
                                $" Game Mode: {mode}\n" +
                                $" Time: {elapsedTime}");
                        GameHistory.History.Add(completedProblem);
                        Menu.TotalProblemsSolved++;
                        // Play again?
                        Console.WriteLine(" Play again?\n" +
                                          " 'Yes' - Generates a new problem\n" +
                                          " 'Menu' returns to menu");
                        Console.Write("   Choice: ");

                        await speech.PlayAgainSpeechInput();
                        var response = Speech.PlayAgainInput;

                        if (response == "menu")
                        {
                            Console.Write("Returning to menu");
                            for (var i = 0; i < 3; i++)
                            {
                                Console.Write(" . ");
                                Thread.Sleep(500);
                            }

                            Console.Clear();
                            guessing = false;
                            isRunning = false;
                            await Menu.MainMenu();
                        }

                        if (response == "yes")
                        {
                            Console.Clear();
                            sessionsFirstQuestion = false;
                            Menu.Attempts = 1;
                        }
                    }
                    else
                    {
                        Menu.Attempts++;
                    }
                }
                catch
                {
                    Console.Write("Failed...Invalid input");
                    Thread.Sleep(600);
                    Console.Clear();
                    tryingAgain = true;
                }
            }
        }
    }
}