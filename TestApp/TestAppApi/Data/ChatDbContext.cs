using Microsoft.EntityFrameworkCore;

namespace TestAppApi.Controllers
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions options) : base(options)
        {
            // Just call base
        }

        public DbSet<ChatUser> Users { get; set; }
        public DbSet<ChatMessage> Messages { get; set; }
    }
}