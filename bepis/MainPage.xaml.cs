using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace bepis
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MediaElement speechMedia = new MediaElement();
        private SpeechSynthesizer synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Bepis machine broke!");
            speechMedia.SetSource(stream, stream.ContentType);
            speechMedia.Play();
            bepisDialog();
        }

        private void textBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void bepisDialog()
        {
            // Create the message dialog and set its content
            var messageDialog = new MessageDialog("Is this understandable to you?");

            // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
            messageDialog.Commands.Add(new UICommand(
                "No!",
                new UICommandInvokedHandler(this.bepisDialogHandler)));
            messageDialog.Commands.Add(new UICommand(
                "Have a great day",
                new UICommandInvokedHandler(this.bepisDialogHandler)));

            // Set the command that will be invoked by default
            messageDialog.DefaultCommandIndex = 1;

            // Set the command to be invoked when escape is pressed
            messageDialog.CancelCommandIndex = 0;

            // Show the message dialog
            await messageDialog.ShowAsync();
        }

        private async void bepisDialogHandler(IUICommand command)
        {
            if (command.Label.Equals("Have a great day"))
            {
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Understandable, have a great day.");
                speechMedia.SetSource(stream, stream.ContentType);
                speechMedia.Play();
            }
        }

    }
}
