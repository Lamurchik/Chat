using Microsoft.EntityFrameworkCore;
namespace ChatLaba3
{
    public class MsgContext : DbContext
    {
        public DbSet<Msg> Msg { get; set; } = null;
        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlServer("Server=localhost;Database=chat;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}
