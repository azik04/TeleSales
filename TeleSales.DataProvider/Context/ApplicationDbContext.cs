using Microsoft.EntityFrameworkCore;
using TeleSales.DataProvider.Entities;

namespace TeleSales.DataProvider.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Users> Users { get; set; }
    public DbSet<Kanals> Kanals { get; set; }
    public DbSet<Calls> Calls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Calls -> Kanals (Many-to-One)
        modelBuilder.Entity<Calls>()
            .HasOne(c => c.Kanal)
            .WithMany(k => k.Calls)
            .HasForeignKey(c => c.KanalId)
            .OnDelete(DeleteBehavior.Restrict);

        // Calls -> Users (Many-to-One, optional relationship)
        modelBuilder.Entity<Calls>()
            .HasOne(c => c.User)
            .WithMany(u => u.Calls)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasData(new Users
            {
                Email = "admin@adra.gov.az",
                FullName = "Admin",
                Password = "Admin123",
                id = 1,
                Role = Enums.Role.Admin,
                CreateAt = DateTime.Now,
                isDeleted = false,
            });
        });

        base.OnModelCreating(modelBuilder);
    }

}
