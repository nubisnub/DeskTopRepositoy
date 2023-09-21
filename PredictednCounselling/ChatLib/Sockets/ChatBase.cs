using System.Net;

namespace CounsellingChatLib.Sockets
{
    public abstract class ChatBase : Event.Event
    {
        protected readonly IPAddress IPAddress;
        protected readonly int Port;

        private bool _isRunning;


        public event Action<bool>? RunningStateChanged;

        public ChatBase(IPAddress iPAddress, int port)
        {
            IPAddress = iPAddress;
            Port = port;
        }

        public bool IsRunning
        {
            get=>_isRunning; 
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    RunningStateChanged?.Invoke(_isRunning);
                }
            }
        }
    }
}
