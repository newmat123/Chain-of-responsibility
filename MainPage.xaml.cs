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
    public interface ITextHandler
    {
        ITextHandler SetNext(ITextHandler handler);

        object Handle(char character);
    }

    public abstract class AbstractTextHandler : ITextHandler
    {
        private ITextHandler _nextHandler;
       
        public ITextHandler SetNext(ITextHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }

        public virtual object Handle(char character)
        {
            if (this._nextHandler != null)
            { 
                return this._nextHandler.Handle(character);
            }
            return character;
        }
    }

    public class SingleQuoteHandler : AbstractTextHandler
    {

        public override object Handle(char character)
        {
            if (character == '\'')
            {
                return "specialCharacter";
            }
            return base.Handle(character);
        }
    }

    public class NumberHandler : AbstractTextHandler
    {
        public override object Handle(char character)
        {
            if (char.IsDigit(character))
            {
                return "number";
            }
            return base.Handle(character);
        }
    }

    public class LetterHandler : AbstractTextHandler
    {
        public override object Handle(char character)
        {
            if (char.IsLetter(character))
            {
                return "character";
            }
            return base.Handle(character);
        }
    }
    public partial class MainPage : ContentPage
    {
        private ITextHandler textInputHandler;
        private AbstractTextHandler singleQuoteHandler;
        private AbstractTextHandler numberHandler;
        private AbstractTextHandler letterHandler;

        public MainPage()
        {
            InitializeComponent();

            singleQuoteHandler = new SingleQuoteHandler();
            numberHandler = new NumberHandler();
            letterHandler = new LetterHandler();

            singleQuoteHandler.SetNext(numberHandler).SetNext(letterHandler);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string inputText = e.NewTextValue;
            if (inputText.Length > 0)
            {
                var handled = singleQuoteHandler.Handle(inputText[inputText.Length - 1]);
                switch (handled as string)
                {
                    case "number":
                        HelpText.Text = "You entered a number";
                        break;
                    case "specialCharacter":
                        HelpText.Text = "You entered a '";
                        break;
                    case "character":
                        HelpText.Text = "You entered a character";
                        break;
                    default:
                        HelpText.Text = "Unhandled character";
                        break;
                }
            }
        }
    }

}