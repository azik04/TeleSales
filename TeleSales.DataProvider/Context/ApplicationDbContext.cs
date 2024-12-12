using Microsoft.EntityFrameworkCore;
using TeleSales.DataProvider.Entities;

namespace TeleSales.DataProvider.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<Users> Users { get; set; }
    public DbSet<Kanals> Kanals { get; set; }
    public DbSet<Calls> Calls { get; set; }
    public DbSet<UserKanals> UserKanals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calls>()
            .HasOne(c => c.Kanal)
            .WithMany(k => k.Calls)
            .HasForeignKey(c => c.KanalId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Calls>()
            .HasOne(c => c.User)
            .WithMany(u => u.Calls)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserKanals>()
            .HasKey(uk => new { uk.UserId, uk.KanalId }); 

        modelBuilder.Entity<UserKanals>()
            .HasOne(uk => uk.Users)
            .WithMany(u => u.UserKanal)
            .HasForeignKey(uk => uk.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserKanals>()
            .HasOne(uk => uk.Kanals)
            .WithMany(k => k.UserKanal)
            .HasForeignKey(uk => uk.KanalId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasData(new Users
            {
                id = 1,
                Email = "admin@adra.gov.az",
                FullName = "Admin",
                Password = "Admin123",
                Role = Enums.Role.Admin,
                CreateAt = DateTime.Now,
                isDeleted = false,
            });
        });

        base.OnModelCreating(modelBuilder);
    }

}
