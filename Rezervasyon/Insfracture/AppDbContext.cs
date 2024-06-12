using Microsoft.EntityFrameworkCore;

namespace Rezervasyon.Insfracture
{
    public class AppDbContext : DbContext 
    { 
        public AppDbContext(DbContextOptions<AppDbContext> context) : base(context) 
        { } 
        public AppDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Server=TARIK23_\\SQLEXPRESS01;Database=Rezarvasyon;Trusted_Connection=True;Encrypt=False;");

        }
    }
}
