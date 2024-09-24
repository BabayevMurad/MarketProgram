using MarketProgram.Library.Models;
using MarketProgram.Library.Services;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuyHistory>()
                .HasMany(c => c.Products)
                .WithOne(p => p.BuyHistory)
                .HasForeignKey(p => p.BuyHistoryId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Basket)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            //modelBuilder.Entity<Admin>()
            //    .HasData
            //    (
            //        new Admin
            //        (
            //            "Murad",
            //            "Babayev",
            //            "babay_aq38",
            //            "0503167673"
            //        )
            //    );

            //modelBuilder.Entity<Product>()
            //    .HasData
            //    (
            //        new Category("Un", new List<Product> {
            //            new Product("Çörək", 0.55, "Ağ Çörək.",10,"Un"),
            //            new Product("Bulka", 0.6, "Kişmişli.",5, "Un"),
            //            new Product("Çörək", 1, "Qara Çörək.",3, "Un"),
            //        }),
            //        new Category("Süd", new List<Product> {
            //            new Product("Süd", 1.2, "15% 1L.",6,"Süd"),
            //            new Product("Pendir", 0.9, "İvanovka.",2,"Süd"),
            //            new Product("Yağ", 16, "Kərə.",9, "Süd"),
            //        })
            //    );
        }
    }
}
