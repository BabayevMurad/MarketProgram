using MarketProgram.Library.Models;
using Microsoft.EntityFrameworkCore;
using MarketProgram.Library.Models.Dto_s;

namespace MarketProgram.Library.Data
{
    public class MarketAppContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = MURAD; Initial Catalog = MarketProgramDb; Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate=True; Application Intent = ReadWrite; Multi Subnet Failover=False\r\n");
        }

        public DbSet<Admin> Admin { get; set; }
        
        public DbSet<BuyHistory> BuyHistory { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Prices> Price { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<BuyHistoryProduct> BuyHistoryProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BuyHistoryProduct>().HasKey(by => new { by.ProductId, by.BuyHistoryId });

            modelBuilder.Entity<BuyHistoryProduct>()
                .HasOne(byp => byp.Product)
                .WithMany(p => p.BuyHistories)
                .HasForeignKey(byp => byp.ProductId);

            modelBuilder.Entity<BuyHistoryProduct>()
                .HasOne(byp => byp.BuyHistory)
                .WithMany(by => by.ByProducts)
                .HasForeignKey(byp => byp.BuyHistoryId);
                
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

        }
    }
}
