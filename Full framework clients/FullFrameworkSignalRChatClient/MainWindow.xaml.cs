using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace FullFrameworkSignalRChatClient
{
    public partial class MainWindow : Window
    {
        private HubConnection connection;
        private IHubProxy hubProxy;

        public MainWindow()
        {
            InitializeComponent();

            
            connection = new HubConnection(" https://localhost:7144/");
            hubProxy = connection.CreateHubProxy("MyHub");

           
            connection.Closed += async () =>
            {

                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.Start();
            };
        }

        private async void connectButton_Click(object sender, RoutedEventArgs e)
        {
            hubProxy.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Dispatcher.Invoke(() =>
                {
                    var newMessage = $"{user}: {message}";
                    messagesList.Items.Add(newMessage);
                });
            });

            try
            {
                await connection.Start();
                messagesList.Items.Add("Connection started");
                connectButton.IsEnabled = false;
                sendButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await hubProxy.Invoke("SendMessage", userTextBox.Text, messageTextBox.Text);
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }
    }
}
