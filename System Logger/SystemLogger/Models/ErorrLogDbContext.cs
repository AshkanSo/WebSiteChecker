using Microsoft.EntityFrameworkCore;

namespace SystemLogger.Models
{
    public class ErorrLogDbContext : DbContext
    {
        public ErorrLogDbContext(DbContextOptions<ErorrLogDbContext> options) : base(options)
        {
        }
        public DbSet<ErorrLogs> ErorrLogs { get; set; }
        public DbSet<PhoneNumbers> PhoneNumbers { get; set; }
        public DbSet<Website> WebSitesNames { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ErorrLogs>().HasKey(e => e.PK_ErrorLog);
            modelBuilder.Entity<PhoneNumbers>().HasKey(p => p.PK_PhoneNumber);
            modelBuilder.Entity<Website>().HasKey(p => p.PK_Website);
            
            modelBuilder.Entity<ErorrLogs>()
                .HasMany(e => e.PhoneNumbers)
                .WithMany(p => p.ErorrLogs)
                .UsingEntity<Dictionary<string, object>>(
                    "ErorrLogsPhoneNumbers",
                    j => j.HasOne<PhoneNumbers>().WithMany().HasForeignKey("PhoneNumbersId"),
                    j => j.HasOne<ErorrLogs>().WithMany().HasForeignKey("ErorrLogsId"));

            
            modelBuilder.Entity<PhoneNumbers>().HasData(
                new PhoneNumbers { PK_PhoneNumber = 1, Name = "Contact1", PhoneNumber = "09131111111" });
            modelBuilder.Entity<PhoneNumbers>().HasData(
                new PhoneNumbers { PK_PhoneNumber = 2, Name = "Contact1", PhoneNumber = "09132222222" });


            modelBuilder.Entity<Website>().HasData(new Website
                { PK_Website = 1, Name = "Fadia", Url = "https://fadiashop.com/wakeup" ,ServerStatus = true,FK_PhoneNumbers = 1});
            modelBuilder.Entity<Website>().HasData(new Website
                { PK_Website = 2, Name = "LocalHost", Url = "https://localhost:44304/api/TestUrl/Test" , ServerStatus = true,FK_PhoneNumbers = 2});
            
        }
    }
}