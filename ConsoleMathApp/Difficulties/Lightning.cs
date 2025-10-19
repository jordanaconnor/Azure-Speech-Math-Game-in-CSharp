namespace ConsoleMathApp;

public class Lightning
{
    public static async Task LightningMode(string mode)
    {
        var isRunning = true;
        var rand = new Random();
        var speech = new Speech();

        while (isRunning)
        {
            var guessing = true;

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
                        Console.WriteLine("randomOperatorSelector did something other than 1-4");
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

                if (Menu.OperatorSymbol == '-')
                {
                    while (a < b)
                    {
                        a = MathLogic.RandomNum(mode);
                        b = MathLogic.RandomNum(mode);
                    }

                    Menu.Answer = MathLogic.Subtract(a, b);
                }

                while (looping)
                {
                    Console.WriteLine();
                    Console.WriteLine($"          Problem {Menu.TotalProblemsSolved}");
                    Console.WriteLine(" ───────────────────────────");
                    Console.WriteLine(" ───────────────────────────");
                    MathLogic.PrintProblem(a, b, Menu.OperatorSymbol);


                    Console.WriteLine(" ───────────────────────────");
                    Console.WriteLine($"Correct Answer: {Menu.Answer}");
                    Console.Write("    Guess: ");

                    try
                    {
                        await speech.GetGuessSpeechInput();
                        var guess = Speech.NumGuess;

                        if (guess == Menu.Answer)
                        {
                            // Add to Memory List
                            var completedProblem =
                                Convert.ToString(
                                    $"Question {Menu.TotalProblemsSolved}: \n " +
                                    $"{a} {Menu.OperatorSymbol} {b} = {Menu.Answer} \n" +
                                    $" Attempts: {Menu.Attempts}\n" +
                                    $" Game Mode: Lightning\n" +
                                    $" Question difficulty: {mode}");
                            GameHistory.History.Add(completedProblem);
                            Menu.TotalProblemsSolved++;

                            Console.WriteLine("          Correct");
                            Console.WriteLine();
                            Console.WriteLine(" Play again?\n" +
                                              " 'Yes' - Generates a new problem\n" +
                                              " 'Menu' returns to menu");
                            Console.Write("    Choice: ");

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
                                looping = false;
                                await Menu.MainMenu();
                            }

                            if (response == "yes")
                            {
                                Console.Clear();
                                looping = false;
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
                        Console.WriteLine("          Try again...");
                        Thread.Sleep(600);
                        Console.Clear();
                    }
                }
            }
        }
    }
}