using CounsellingChatLib.Event;
using CounsellingChatLib.Model;
using CounsellingChatLib.Sockets;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;

namespace CounsellingChatServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChatServer _server;
        private ChatRoomManager _chatRoomManager;
        private ObservableCollection<string> messages = new ObservableCollection<string>();
        
        private ChatHub NewChatHub(ChatHub hub, ChatState state)
        {
            return new ChatHub {
                RoomId = hub.RoomId,
                ChatNickName = hub.ChatNickName,
                State = state,
            };
        }

        private void AddClientMessageListBox(ChatHub hub)
        {
            string message = hub.State switch
            {
                ChatState.Connect => $"*접속*{hub}*",
                ChatState.Disconnect => $"*종료*{hub}*",
                _ => $"{hub}: {hub.Message}  (보낸시간 {DateTime.Now.ToString("HH:mm")})"
            };
            messages.Add(message);

            
            MessagesListBox.ScrollIntoView(message);
        }

        private void Connected(object? sender, ChatEventArgs e)
        {
            var hub = NewChatHub(e.Hub, ChatState.Connect);

            _chatRoomManager.Add(e.ClientHandler);
            _chatRoomManager.SendToMyRoom(hub);

            ConnectedClientListBox.Items.Add(e.Hub);

           
            ConnectedClientListBox.ScrollIntoView(e.Hub);

            AddClientMessageListBox(hub);
        }

        private void Disconnected(object? sender, ChatEventArgs e)
        {
            var hub = NewChatHub(e.Hub, ChatState.Disconnect);

            _chatRoomManager.Remove(e.ClientHandler);
            _chatRoomManager.SendToMyRoom(hub);

            ConnectedClientListBox.Items.Remove(e.Hub);

            
            ConnectedClientListBox.ScrollIntoView(e.Hub);

            AddClientMessageListBox(hub);
        }

        private void Received(object? sender, ChatEventArgs e)
        {
            _chatRoomManager.SendToMyRoom(e.Hub);

            AddClientMessageListBox(e.Hub);
        }

        private void RunningStateChanged(bool isRunning)
        {
            StartBtn.IsEnabled = !isRunning;
            StopBtn.IsEnabled = isRunning;
        }
        private void StartBtn_Click(object? sender, EventArgs e)
        {
            _ = _server.StartAsync();
        }

        private void StopBtn_Click(object? sender, EventArgs e)
        {
            _server.Stop();
        }

        public MainWindow()
        {
            InitializeComponent();

            _chatRoomManager = new ChatRoomManager();
            _server = new ChatServer(IPAddress.Parse("127.0.0.1"), 8080);
            _server.Connected += Connected;
            _server.Disconnected += Disconnected;
            _server.Received += Received;
            _server.RunningStateChanged += RunningStateChanged;

            StartBtn.Click += StartBtn_Click;
            StopBtn.Click += StopBtn_Click;
            MessagesListBox.ItemsSource = messages;
            
        }
    }
}
