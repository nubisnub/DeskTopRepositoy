using CounsellingChatLib.Event;
using CounsellingChatLib.Model;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace CounsellingChatLib.Sockets
{
    public class ChatClient : ChatBase
    {
        private TcpClient? _client;

        public override event EventHandler<ChatEventArgs>? Connected;
        public override event EventHandler<ChatEventArgs>? Disconnected;
        public override event EventHandler<ChatEventArgs>? Received;

        //문의하기 버튼 누르기 전 이름에 쓰인 string타입을 ChatNickName으로 꼭 변경할 것.
        private static ChatHub SetUpHub(ChatInfo chatInfo)
        {
            return new ChatHub
            {
                UserId = chatInfo.UserId,
                RoomId = chatInfo.RoomId,
                ChatNickName = chatInfo.ChatNickName,
                State = ChatState.Initial,
                
            };
        }
        public ChatClient(IPAddress iPAddress, int port) : base(iPAddress, port) { }

        public async Task ConnectAsync(ChatInfo chatInfo)
        {
            if (IsRunning) return;

            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(IPAddress, Port);
                IsRunning = true;

                ChatHub hub =SetUpHub(chatInfo);
                ClientHandler clientHandler = new(_client);
                Connected?.Invoke(this, new ChatEventArgs(clientHandler, hub));
                clientHandler.Disconnected += ClientHandler_Disconnected;
                clientHandler.Received += Received;

                _ = clientHandler.HandleClientAsync();
                clientHandler?.Send(hub);
            }
            catch (Exception e)
            {
                Debug.Print("서버 연결 시도 중 오류 발생 :  " + e.Message);
              
            }
        }

        private void DisposeClient()
        {
            _client?.Dispose();
            IsRunning = false;
        }


        //ClientHandler_Disconnected 정의
        private void ClientHandler_Disconnected(object? sender, ChatEventArgs e)
        {
            DisposeClient();
            Disconnected?.Invoke(sender, e);
        }

        public void Close()
        {
            DisposeClient();
        }

    }
}
