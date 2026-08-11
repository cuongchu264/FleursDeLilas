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
        public DbSet<Flower> Flowers { get; set; } = null!;
        public DbSet<Supply> Supplies { get; set; } = null!;
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

            // Configure mapping for flower table
            modelBuilder.Entity<Flower>(entity =>
            {
                entity.ToTable("flower");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("flo_name")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Price)
                    .HasColumnName("flo_price")
                    .HasDefaultValue(0);

                entity.Property(e => e.TotalCount)
                    .HasColumnName("flo_toal_count")
                    .HasDefaultValue(0);

                entity.Property(e => e.AvailableCount)
                    .HasColumnName("flo_avaiable_count")
                    .HasDefaultValue(0);

                entity.Property(e => e.FailedCount)
                    .HasColumnName("flo_failed_count")
                    .HasDefaultValue(0);

                entity.Property(e => e.BuyDate)
                    .HasColumnName("flo_buy_date");

                entity.Property(e => e.Note)
                    .HasColumnName("flo_note")
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Configure mapping for supply table
            modelBuilder.Entity<Supply>(entity =>
            {
                entity.ToTable("supply");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("sup_name")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Price)
                    .HasColumnName("sup_price")
                    .HasDefaultValue(0);

                entity.Property(e => e.Count)
                    .HasColumnName("sup_count")
                    .HasDefaultValue(0);

                entity.Property(e => e.Note)
                    .HasColumnName("sup_note")
                    .HasMaxLength(500);

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
