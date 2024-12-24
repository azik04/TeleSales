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
    public DbSet<UserKanals> UserKanals { get; set; }
    public DbSet<CallCenters> CallCenters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calls>()
            .HasOne(c => c.Kanal)
            .WithMany(k => k.Calls)
            .HasForeignKey(c => c.KanalId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Calls>(entity =>
        {
            entity.Property(e => e.TotalDebt).HasPrecision(18, 2);
            entity.Property(e => e.Year2018).HasPrecision(18, 2);
            entity.Property(e => e.Year2019).HasPrecision(18, 2);
            entity.Property(e => e.Year2020).HasPrecision(18, 2);
            entity.Property(e => e.Year2021).HasPrecision(18, 2);
            entity.Property(e => e.Year2022).HasPrecision(18, 2);
            entity.Property(e => e.Year2023).HasPrecision(18, 2);
            entity.Property(e => e.Month1_2024).HasPrecision(18, 2);
            entity.Property(e => e.Month2_2024).HasPrecision(18, 2);
            entity.Property(e => e.Month3_2024).HasPrecision(18, 2);
            entity.Property(e => e.Month4_2024).HasPrecision(18, 2);
            entity.Property(e => e.Month5_2024).HasPrecision(18, 2);
            entity.Property(e => e.Month6_2024).HasPrecision(18, 2);
            entity.Property(e => e.Month7_2024).HasPrecision(18, 2);
            entity.Property(e => e.Month8_2024).HasPrecision(18, 2);
            entity.Property(e => e.Month9_2024).HasPrecision(18, 2);
            entity.Property(e => e.Month10_2024).HasPrecision(18, 2);
            entity.Property(e => e.Month11_2024).HasPrecision(18, 2);
            entity.Property(e => e.Month12_2024).HasPrecision(18, 2);
            entity.Property(e => e.Month1_2025).HasPrecision(18, 2);
            entity.Property(e => e.Month2_2025).HasPrecision(18, 2);
            entity.Property(e => e.Month3_2025).HasPrecision(18, 2);

            entity.Property(e => e.TotalDebt).HasColumnType("decimal(18,2)");

        });
        modelBuilder.Entity<Calls>()
            .HasOne(c => c.User)
            .WithMany(u => u.Calls)
            .HasForeignKey(c => c.ExcludedBy)
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
        modelBuilder.Entity<CallCenters>()
            .HasOne(cc => cc.Users) 
            .WithMany(u => u.CallCenter) 
            .HasForeignKey(cc => cc.ExcludedBy)
            .OnDelete(DeleteBehavior.Restrict);


        base.OnModelCreating(modelBuilder);
    }

}
