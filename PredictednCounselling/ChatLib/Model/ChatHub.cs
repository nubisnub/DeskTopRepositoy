using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CounsellingChatLib.Model
{
    //전송하는 문장에 이름을 추가하여 jsonserializer로 Serialize , Deserialize
    public class ChatHub : ChatInfo   // 현재 ChatInfo에는 NickName 하나만 존재. 혹시모르니..
    {
        public static ChatHub? Parse(string json) => JsonSerializer.Deserialize<ChatHub>(json)!;
        public ChatState State { get; set; } 
        public string Message { get; set; } = string.Empty;
        public string ToJsonString() => JsonSerializer.Serialize(this);
    }
}
