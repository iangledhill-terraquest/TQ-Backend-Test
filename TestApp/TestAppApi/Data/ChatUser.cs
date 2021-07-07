using System.Collections.Generic;

namespace TestAppApi.Controllers
{
    public class ChatUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<ChatMessage> ChatMessage { get; set; }

    }
}