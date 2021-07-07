using System.Collections.Generic;

namespace TestAppApi.Controllers
{
    public class ChatRepository
    {
        private static List<ChatMessage> _chatMessage = new List<ChatMessage>();
        private static ChatRepository _chatRepository = new ChatRepository();

        public ChatRepository()
        {
            
        }

        public static ChatRepository Instance => _chatRepository;

        public List<ChatMessage> Messages => _chatMessage;
    }
}