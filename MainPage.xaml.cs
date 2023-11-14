//using CoreGraphics;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Diagnostics;
using System.Linq;

namespace Chain_of_responsibility
{
    // The Handler interface declares a method for building the chain of
    // handlers. It also declares a method for executing a request.
    public interface ITextHandler
    {
        ITextHandler SetNext(ITextHandler handler);

        void Handle(char character);
    }

    // The default chaining behavior can be implemented inside a base handler
    // class.
    public abstract class AbstractTextHandler : ITextHandler
    {
        private ITextHandler _nextHandler;

        public ITextHandler SetNext(ITextHandler handler)
        {
            this._nextHandler = handler;

            // Returning a handler from here will let us link handlers in a
            // convenient way.
            return handler;
        }

        public virtual void Handle(char character)
        {
            if (this._nextHandler != null)
            {
                this._nextHandler.Handle(character);
            }
        }
    }

    public class SingleQuoteHandler : AbstractTextHandler
    {
        private Entry textInput;

        public SingleQuoteHandler(Entry textInput)
        {
            this.textInput = textInput;
        }

        public override void Handle(char character)
        {
            if (character == '\'')
            {
                // Handle the single quote character
                Console.WriteLine("Single Quote Handler: Removing the single quote character.");
                // Update the Entry text to remove the single quote character
                textInput.Text = textInput.Text.Replace("'", "");
            }
            else
            {
                base.Handle(character);
            }
        }
    }

    public class NumberHandler : AbstractTextHandler
    {
        public override void Handle(char character)
        {
            if (char.IsDigit(character))
            {
                // Handle numeric characters
                Console.WriteLine("Number Handler: Handling a numeric character.");
            }
            else
            {
                base.Handle(character);
            }
        }
    }

    public class LetterHandler : AbstractTextHandler
    {
        public override void Handle(char character)
        {
            if (char.IsLetter(character))
            {
                // Handle alphabetic characters
                Console.WriteLine("Letter Handler: Handling an alphabetic character.");
            }
            else
            {
                base.Handle(character);
            }
        }
    }
    public partial class MainPage : ContentPage
    {

        private readonly ITextHandler textInputHandler;

        public MainPage()
        {
            InitializeComponent();


            // Create a logger factory with a debug provider
            using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddDebug());

            // Create a logger with the category name of the current class
            ILogger<MainPage> logger = loggerFactory.CreateLogger<MainPage>();

            // Log some messages with different log levels and message templates

            logger.LogDebug("This is a debug message.");

            // Creating the chain: SingleQuoteHandler > NumberHandler > LetterHandler
            var singleQuoteHandler = new SingleQuoteHandler(textInput);
            var numberHandler = new NumberHandler();
            var letterHandler = new LetterHandler();

            textInputHandler = singleQuoteHandler.SetNext(numberHandler).SetNext(letterHandler);
        }

        // Handle the actual Entry TextChanged event
        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            // Create a logger factory with a debug provider
            using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddDebug());

            // Create a logger with the category name of the current class
            ILogger<MainPage> logger = loggerFactory.CreateLogger<MainPage>();

            // Log some messages with different log levels and message templates
           
            logger.LogDebug("This is a debug message.");
            // Simulate text input
            string inputText = e.NewTextValue;

            // Sending each character in the text through the chain
            foreach (char character in inputText)
            {
                Console.WriteLine($"Processing character: {character}");
                textInputHandler.Handle(character);
            }
        }

        //Select the Correct Device/Emulator:

        //Make sure you have a device or emulator selected in the toolbar.
        //For Android, select "Device" and choose your connected device or an emulator.
        //For iOS, select "iOS Simulator" and choose the simulator you're using.
        //Open the Output Window:

        //In Visual Studio, go to View > Output or press Ctrl + W, O to open the Output window.
        //Select the Appropriate Output:

        //In the Output window, there should be a dropdown list to select different outputs (Debug, Release, etc.).
        //Choose the output related to your selected device/emulator.
        //Look for Console Output:

        //In the Output window, look for log entries that contain your Console.WriteLine messages.




        //private readonly ITextHandler textInputHandler;


        //private readonly ITextHandler textInputHandler;

        //public MainPage()
        //{
        //    InitializeComponent();

        //    // Creating the chain: SingleQuoteHandler > NumberHandler > LetterHandler
        //    var singleQuoteHandler = new SingleQuoteHandler();
        //    var numberHandler = new NumberHandler();
        //    var letterHandler = new LetterHandler();

        //    textInputHandler = singleQuoteHandler.SetNext(numberHandler).SetNext(letterHandler);

        //    // Simulating text input
        //    string inputText = "abc'123'xyz";

        //    // Sending each character in the text through the chain
        //    foreach (char character in inputText)
        //    {
        //        Console.WriteLine($"Processing character: {character}");
        //        textInputHandler.Handle(character);
        //    }
        //}

        //// Handle the actual Entry TextChanged event
        //private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        //{
        //    // The text has already been modified by the handlers.
        //    // You can add any additional logic here if needed.
        //}

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    // Find the Entry control by type when the page appears
        //    var entry = this.Descendants<Entry>().FirstOrDefault();

        //    if (entry != null)
        //    {
        //        // Create the chain: SingleQuoteHandler > NumberHandler > LetterHandler
        //        var singleQuoteHandler = new SingleQuoteHandler();
        //        var numberHandler = new NumberHandler();
        //        var letterHandler = new LetterHandler();

        //        var textInputHandler = singleQuoteHandler.SetNext(numberHandler).SetNext(letterHandler);

        //        // Simulate text input
        //        string inputText = entry.Text;

        //        // Sending each character in the text through the chain
        //        foreach (char character in inputText)
        //        {
        //            Console.WriteLine($"Processing character: {character}");
        //            textInputHandler.Handle(character);
        //        }
        //    }
        //}
        //private readonly ITextHandler textInputHandler;
        ////private bool updatingText = false;

        //public MainPage()
        //{
        //    InitializeComponent();

        //    // Creating the chain: SingleQuoteHandler > NumberHandler > LetterHandler
        //    var singleQuoteHandler = new SingleQuoteHandler();
        //    var numberHandler = new NumberHandler();
        //    var letterHandler = new LetterHandler();

        //    textInputHandler = singleQuoteHandler.SetNext(numberHandler).SetNext(letterHandler);

        //    // Simulating text input
        //    string inputText = "abc'123'xyz";

        //    // Sending each character in the text through the chain
        //    foreach (char character in inputText)
        //    {
        //        Console.WriteLine($"Processing character: {character}");
        //        textInputHandler.Handle(character);
        //    }
        //}
        //private void OnButtonClick(object sender, EventArgs e)
        //{
        //    string enteredText = textInput.Text;
        //    // Do something with the entered text
        //    // For example, display it in a label
        //    DisplayAlert("Entered Text", enteredText, "OK");
        //}

        // Handle the actual Entry TextChanged event
        //private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        //{
        //    // The text has already been modified by the handlers.
        //    // You can add any additional logic here if needed.
        //}
        //private void OnEntryCompleted(object sender, EventArgs e)
        //{
        //    // Handle the completion event (Enter key pressed)
        //    string enteredText = textInput.Text;
        //    // Do something with the entered text
        //    // For example, display it in a label
        //    DisplayAlert("Entered Text", enteredText, "OK");
        //}

        //private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        //{
        //    // Creating the chain: SingleQuoteHandler > NumberHandler > LetterHandler
        //    var singleQuoteHandler = new SingleQuoteHandler();
        //    var numberHandler = new NumberHandler();
        //    var letterHandler = new LetterHandler();

        //    singleQuoteHandler.SetNext(numberHandler).SetNext(letterHandler);
        //    singleQuoteHandler.Handle(e.NewTextValue.Last());
        //    if (!updatingText)
        //    {
        //        updatingText = true;

        //        // Check if the entered text contains "'"
        //        if (e.NewTextValue.Contains("'"))
        //        {
        //            // Remove the "'" character
        //            textInput.Text = e.NewTextValue.Replace("'", "");

        //            // Notify the user or perform any other action
        //            DisplayAlert("Special Character Entered", "The single quote character is not allowed.", "OK");
        //        }

        //        updatingText = false;
        //    }
        //}
    }

}