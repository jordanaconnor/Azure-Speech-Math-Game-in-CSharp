# Console Math App

A simple voice-controlled console math game that uses **Azure AI Speech Services** for speech recognition.

The main branch is a simple text based console app created in Jetbrains Rider. I recommend cloning and opening in an IDE of your choice to play the input version.

The AISpeech_working branch is the version with the Azure AI Speech service implemented allowing to play the game with 100% voice commands.
I also recommend you clone and open the program in an IDE and have a working mic connected.
To run the program a key and region needs to be added to the Speech.cs file, that can be located by signing up in Azure and creating a new Ai Speech service.

## Setup Instructions

1. Create an **Azure AI Speech Service** resource in the [Azure Portal](https://portal.azure.com/).
2. Copy your **Speech Service Key** and **Region**.
3. Open the `Speech.cs` file and replace the placeholder values:
   ```csharp
   var key = "YOUR_AZURE_SPEECH_KEY";
   var region = "YOUR_REGION";
