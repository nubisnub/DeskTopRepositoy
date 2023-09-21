using CounsellingChatLib.Model;

namespace CounsellingChatLib.Event
{
    public class ChatRoomManager
    {
       
        private readonly Dictionary<int, List<ClientHandler>> _roomHandlersDict = new();

        public void Add(ClientHandler clientHandler)
        {
            

            int roomId = clientHandler.InitialData!.RoomId;

            if(_roomHandlersDict.TryGetValue(roomId,out _))
            {
                _roomHandlersDict[roomId].Add(clientHandler);
            }
            else
            {
                _roomHandlersDict[roomId] = new List<ClientHandler>() {clientHandler};
            }
        }

        public void Remove(ClientHandler clientHandler)
        {
            int roomId = clientHandler.InitialData!.RoomId;

            if(_roomHandlersDict.TryGetValue(roomId, out List<ClientHandler>? roomHandlers)) {
                _roomHandlersDict[roomId] = roomHandlers.FindAll(handler => !handler.Equals(clientHandler));
            }
        }

        public void SendToMyRoom(ChatHub hub)
        {
            if(_roomHandlersDict.TryGetValue(hub.RoomId, out List<ClientHandler>? roomHandlers))
            {
                roomHandlers.ForEach(handler => handler.Send(hub));
            }
        }

    }
}
