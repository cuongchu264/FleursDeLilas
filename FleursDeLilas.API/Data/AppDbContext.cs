using FleursDeLilas.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FleursDeLilas.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<FleursUser> FleursUsers { get; set; } = null!;
        //public DbSet<Flower> Flowers { get; set; } = null!;
        //public DbSet<Supply> Supplies { get; set; } = null!;
        //public DbSet<Order> Orders { get; set; } = null!;
        //public DbSet<OrderPrepareFlo> OrderPrepareFlos { get; set; } = null!;
        //public DbSet<OrderPrepareSupply> OrderPrepareSupplies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure mapping for fleurs_user table
            modelBuilder.Entity<FleursUser>(entity =>
            {
                entity.ToTable("fleurs_user");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
