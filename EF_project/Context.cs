using Microsoft.EntityFrameworkCore;

namespace EF_project
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Entities.UserEntity> Users { get; set; }
        public DbSet<Entities.ChatEntity> Chats { get; set; }
        public DbSet<Entities.MessageEntity> Messages { get; set; }

        public ApplicationContext(bool needToDelete = false)
        {
            if (needToDelete)
                Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=../../../chats_database.db");
        }
    }
}
