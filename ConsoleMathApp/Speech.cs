using Microsoft.CognitiveServices.Speech;
using DotNetEnv;

namespace ConsoleMathApp;

public class Speech
{
    
    private readonly SpeechConfig config;

    
    
    public Speech()
    {

        Env.Load();

        var key = "";
        var region = "";
        
        config = SpeechConfig.FromSubscription(key, region);
    }
    
    public async Task GetSpeechInput()
    {
        bool continueGettingInput = true;
        while (continueGettingInput)
        {
            using var recongnizer = new SpeechRecognizer(config);
            var result = await recongnizer.RecognizeOnceAsync();

            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                Console.WriteLine(result.Text);
            }

            if (result.Text.ToLower().StartsWith("stop"))
            {
                continueGettingInput = false;
            }
        }
    }
}