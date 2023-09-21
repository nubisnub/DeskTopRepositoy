using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounsellingChatLib.Model
{
    public class ChatInfo
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public string ChatNickName { get; set; } = string.Empty;


        //chatInfo에 아직 어떤 정보를 더 담으면 좋을까 피드백 이후에..

        public override string ToString()
        {
            return $"RoomLocation: {RoomId}, ClientNickName: {ChatNickName}";
        }
    }
}
