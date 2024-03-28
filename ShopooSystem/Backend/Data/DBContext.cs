using DataCommon.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); // Configuring cascade delete behavior
        }
    }
}
