using CounsellingChatLib.Event;
using CounsellingChatLib.Model;
using CounsellingChatLib.Sockets;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace CounsellingChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChatClient _chatClient;
        private ClientHandler? _clientHandler;
        private ObservableCollection<string> messages = new ObservableCollection<string>();
        private string ChatNickName => NickName.Text;
        private string Message => SendMessage.Text;
        private int RoomId => ComboBoxContents.SelectedIndex;
        //QnARoom 이면 0번방,ChatRoom 이면 1번 방 

        private void RunningStateChanged(bool isRunning)
        {
            ConnectBtn.IsEnabled = !isRunning;
            ExitBtn.IsEnabled = isRunning;
        }

        private void Connected(object? sender, CounsellingChatLib.Event.ChatEventArgs e)
        {
            _clientHandler = e.ClientHandler;
        }

        private void Disconnected(object? sender, CounsellingChatLib.Event.ChatEventArgs e)
        {
            _clientHandler = null;
            messages.Add("서버의 연결이 끊어졌습니다");

        }

        private void Received(object? sender, CounsellingChatLib.Event.ChatEventArgs e)
        {
            ChatHub hub = e.Hub;
            string message = hub.State switch
            {
                ChatState.Connect => $"{hub.ChatNickName}님이 접속하였습니다.",
                ChatState.Disconnect => $"{hub.ChatNickName}님이 종료하였습니다.",
                _ => $"{hub.ChatNickName} : {hub.Message} (보낸시간 {DateTime.Now.ToString("HH:mm")})"
            };

            MessageListBox.Items.Add(message);

            //오류있는지확인하기
            MessageListBox.ScrollIntoView(message);
        }

        private async void ConnectBtn_Click(object? sender, EventArgs e)
        {
            await _chatClient.ConnectAsync(new ChatInfo
            {
                //chatInfo에 아직 어떤 정보를 더 담으면 좋을까 피드백 이후에..
                RoomId = RoomId,
                ChatNickName = ChatNickName,
            });
        }

        private void ExitBtn_Click(object? sender, EventArgs e)
        {
            _chatClient.Close();
            this.Close();
        }

        private void Message_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _clientHandler?.Send(new ChatHub
                {
                    RoomId = RoomId,
                    ChatNickName = ChatNickName,
                    Message = Message,
                });
                SendMessage.Clear();
            }
        }



        public MainWindow()
        {
            InitializeComponent();

            _chatClient = new ChatClient(IPAddress.Parse("127.0.0.1"), 8080);
            _chatClient.Connected += Connected;
            _chatClient.Disconnected += Disconnected;
            _chatClient.Received += Received;
            _chatClient.RunningStateChanged += RunningStateChanged;

            ConnectBtn.Click += ConnectBtn_Click;
            ExitBtn.Click += ExitBtn_Click;

            SendMessage.KeyDown += Message_KeyDown;
        }
    }
}

