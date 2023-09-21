using CounsellingChatLib.Model;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace CounsellingChatLib.Event
{
    public class ClientHandler:Event
    {

        private readonly TcpClient _client;
        private readonly NetworkStream _stream;


        public override event EventHandler<ChatEventArgs>? Connected;
        public override event EventHandler<ChatEventArgs>? Disconnected;
        public override event EventHandler<ChatEventArgs>? Received;


        public ChatHub? InitialData { get; private set; }


        public ClientHandler(TcpClient client)
        {
            _client = client;
            _stream = client.GetStream();

        }
        public async Task HandleClientAsync()
        {
            byte[] sizeBuffer = new byte[4];
            int read;

            try
            {
                while (true)
                {
                    //initial disconnect connect 이벤트 설정

                    read = await _stream.ReadAsync(sizeBuffer, 0, sizeBuffer.Length);
                    
                    if (read == 0) break; //


                    int size = BitConverter.ToInt32(sizeBuffer);
                    byte[] buffer = new byte[size];

                    read = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (read == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, read);

                    var hub = ChatHub.Parse(message)!;

                    if (hub.State == ChatState.Initial)
                    {
                        InitialData = hub;
                        Debug.Print("클라이언트 연결이벤트 발생");
                        Connected?.Invoke(this, new ChatEventArgs(this, hub));
                    }
                    else
                    {
                        Debug.Print("클라이언트 데이터수신 이벤트 발생");
                        Received?.Invoke(this, new ChatEventArgs(this, hub));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("클라이언트 요청 처리 중 오류 발생: "+ex.Message);
            }
            finally
            {
                _client.Close();
                Disconnected?.Invoke(this, new ChatEventArgs(this, InitialData!));
            }

        }
        public void Send(ChatHub hub)
        {
            //실제 메시지를 보내는 처리과정 메시지를 받아서 길이를 설정 후
            //두 정보를 동시에 보내서 길이만큼 읽도록!

            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(hub.ToJsonString());
                byte[] lengthBuffer = BitConverter.GetBytes(buffer.Length);

                _stream.Write(lengthBuffer);
                _stream.Write(buffer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("클라이언트 요청 처리 중 오류 발생: " + ex.Message);
            }
        }

    }
}
