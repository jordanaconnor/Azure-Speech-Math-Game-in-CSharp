using System.Text.RegularExpressions;
using DotNetEnv;
using Microsoft.CognitiveServices.Speech;

namespace ConsoleMathApp;

public class Speech
{
    public static string UserGuess;
    public static int NumGuess;
    public static string PlayAgainInput;
    public static string LightningModeSelection;

    private readonly SpeechConfig config;
    public bool ContinueGettingInput = true;
    public string UserChoice;

    public Speech()
    {
        Env.Load();

        
        //Input your copied Azure AI Speech Key below between the ""
        var key = "";
        //Input your copied Azure AI Speech region (i.e. centralus) below between the ""
        var region = "";
        config = SpeechConfig.FromSubscription(key, region);
    }

    public async Task GetMainMenuSpeechInput()
    {
        ContinueGettingInput = true;
        var recognizer = new SpeechRecognizer(config);
        try
        {
            while (ContinueGettingInput)
            {
                var result = await recognizer.RecognizeOnceAsync();

                if (result.Reason == ResultReason.RecognizedSpeech)
                    UserChoice = Regex.Replace(result.Text.Trim().ToLower(), @"[^a-z0-9\s]", "");
                // Console.WriteLine(result.Text);
                // Console.WriteLine(UserChoice);
                switch (UserChoice)
                {
                    case "easy":
                        ContinueGettingInput = false;
                        Console.Clear();
                        break;
                    case "medium":
                        ContinueGettingInput = false;
                        Console.Clear();
                        break;
                    case "hard":
                        ContinueGettingInput = false;
                        Console.Clear();
                        break;
                    case "lightning":
                        ContinueGettingInput = false;
                        Console.Clear();
                        break;
                    case "history":
                        ContinueGettingInput = false;
                        Console.Clear();
                        break;
                    case "quit":
                        ContinueGettingInput = false;
                        Console.Clear();
                        break;
                    default:
                        ContinueGettingInput = false;
                        Console.WriteLine("Invalid.");
                        Thread.Sleep(500);
                        break;
                }
            }
        }
        finally
        {
            //safely close the listener and prevent new listeners from initializing
            await recognizer.StopContinuousRecognitionAsync();
            recognizer.Dispose();
        }
    }

    public async Task GetSubMenuSpeechInput()
    {
        ContinueGettingInput = true;
        var recognizer = new SpeechRecognizer(config);
        try
        {
            while (ContinueGettingInput)
            {
                var result = await recognizer.RecognizeOnceAsync();

                if (result.Reason == ResultReason.RecognizedSpeech)
                    UserChoice = Regex.Replace(result.Text.Trim().ToLower(), @"[^a-z0-9\s]", "");
                // Console.WriteLine(result.Text);
                // Console.WriteLine(UserChoice);
                switch (UserChoice)
                {
                    case "add":
                        ContinueGettingInput = false;
                        Console.Clear();
                        break;
                    case "subtract":
                        ContinueGettingInput = false;
                        Console.Clear();
                        break;
                    case "multiply":
                        ContinueGettingInput = false;
                        Console.Clear();
                        break;
                    case "divide":
                        ContinueGettingInput = false;
                        Console.Clear();
                        break;
                    case "menu":
                        ContinueGettingInput = false;
                        Console.Clear();
                        break;
                    case "quit":
                        ContinueGettingInput = false;
                        Console.Clear();
                        break;
                    default:
                        ContinueGettingInput = false;
                        Console.WriteLine("Invalid.");
                        Thread.Sleep(500);
                        break;
                }
            }
        }
        finally
        {
            //safely close the listener and prevent new listeners from initializing
            await recognizer.StopContinuousRecognitionAsync();
            recognizer.Dispose();
        }
    }

    public async Task GetGuessSpeechInput()
    {
        ContinueGettingInput = true;
        var recognizer = new SpeechRecognizer(config);
        try
        {
            while (ContinueGettingInput)
            {
                var result = await recognizer.RecognizeOnceAsync();
                var currentLineCursor = Console.CursorTop;

                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    UserGuess = Regex.Replace(result.Text.Trim().ToLower(), @"[^a-z0-9\s]", "");
                    // Console.WriteLine(result.Text);

                    NumGuess = Convert.ToInt32(UserGuess);
                    if (NumGuess == Menu.Answer)
                    {
                        Console.SetCursorPosition(0, currentLineCursor);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, currentLineCursor);
                        Console.Write($"    Guess: {NumGuess} \n");
                        ContinueGettingInput = false;
                    }
                    else
                    {
                        Console.Write($"Try again...(Heard: {NumGuess})");
                        Thread.Sleep(1000);
                        Console.SetCursorPosition(0, currentLineCursor);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, currentLineCursor);
                        Console.Write("    Guess: ");
                        Menu.Attempts++;
                    }
                }
            }
        }
        finally
        {
            //safely close the listener and prevent new listeners from initializing
            await recognizer.StopContinuousRecognitionAsync();
            recognizer.Dispose();
        }
    }

    public async Task PlayAgainSpeechInput()
    {
        ContinueGettingInput = true;
        var recognizer = new SpeechRecognizer(config);
        try
        {
            while (ContinueGettingInput)
            {
                var result = await recognizer.RecognizeOnceAsync();
                var currentLineCursor = Console.CursorTop;

                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    PlayAgainInput = Regex.Replace(result.Text.Trim().ToLower(), @"[^a-z0-9\s]", "");
                    // Console.WriteLine(result.Text);
                    Console.WriteLine(PlayAgainInput);

                    switch (PlayAgainInput)
                    {
                        case "yes":
                        case "menu":
                            ContinueGettingInput = false;
                            break;
                        default:
                            Console.Write($"Try again...(Heard: {PlayAgainInput})");
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(0, currentLineCursor);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, currentLineCursor);
                            Console.Write("   Choice: ");
                            break;
                    }
                }
            }
        }
        finally
        {
            //safely close the listener and prevent new listeners from initializing
            await recognizer.StopContinuousRecognitionAsync();
            recognizer.Dispose();
        }
    }

    public async Task ReturnFromHistory()
    {
        ContinueGettingInput = true;
        var recognizer = new SpeechRecognizer(config);
        try
        {
            while (ContinueGettingInput)
            {
                var result = await recognizer.RecognizeOnceAsync();
                var currentLineCursor = Console.CursorTop;

                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    var menuInput = Regex.Replace(result.Text.Trim().ToLower(), @"[^a-z0-9\s]", "");
                    // Console.WriteLine(result.Text);
                    Console.WriteLine(menuInput);

                    if (menuInput == "menu")
                    {
                        Console.Write("Returning to menu");
                        for (var i = 0; i < 3; i++)
                        {
                            Console.Write(" . ");
                            Thread.Sleep(500);
                        }

                        Console.Clear();
                        await recognizer.StopContinuousRecognitionAsync();
                        recognizer.Dispose();
                        await Menu.MainMenu();
                    }
                    else
                    {
                        Console.Write($"Try again...(Heard: {PlayAgainInput})");
                        Thread.Sleep(1000);
                        Console.SetCursorPosition(0, currentLineCursor);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, currentLineCursor);
                        Console.Write("   Choice: ");
                    }
                }
            }
        }
        finally
        {
            //safely close the listener and prevent new listeners from initializing
            await recognizer.StopContinuousRecognitionAsync();
            recognizer.Dispose();
        }
    }

    public async Task LightningModeMenuSelection()
    {
        ContinueGettingInput = true;
        var recognizer = new SpeechRecognizer(config);
        try
        {
            while (ContinueGettingInput)
            {
                var result = await recognizer.RecognizeOnceAsync();
                var currentLineCursor = Console.CursorTop;

                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    LightningModeSelection = Regex.Replace(result.Text.Trim().ToLower(), @"[^a-z0-9\s]", "");
                    // Console.WriteLine(result.Text);
                    Console.WriteLine(LightningModeSelection);

                    switch (LightningModeSelection)
                    {
                        case "begin":
                            LightningModeSelection = "begin";
                            ContinueGettingInput = false;
                            break;
                        case "menu":
                            LightningModeSelection = "menu";
                            ContinueGettingInput = false;
                            break;
                        case "quit":
                            LightningModeSelection = "quit";
                            ContinueGettingInput = false;
                            break;
                        default:
                            Console.Write($"Try again...(Heard: {PlayAgainInput})");
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(0, currentLineCursor);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, currentLineCursor);
                            Console.Write(" Menu selection: ");
                            break;
                    }
                }
            }
        }
        finally
        {
            //safely close the listener and prevent new listeners from initializing
            await recognizer.StopContinuousRecognitionAsync();
            recognizer.Dispose();
        }
    }
}