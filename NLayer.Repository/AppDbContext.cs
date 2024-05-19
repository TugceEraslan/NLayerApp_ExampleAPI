using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext: DbContext   // AppDbContext, DbContext sınıfından miras aldı
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  // Veritabanı yolunu startup.cs dosyasından verebilmek için DbContextOptions ctor'unu  oluşturdum.
        {
            
        }

        /* Core katmanım da ki entitylere karşılık gelen DbSet oluşturacağım*/
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        /* Model oluşurken çalışacak olan metodum --> override void OnModelCreating*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //  Assembly.GetExecutingAssembly() ile çalışmış olduğum Assembly'i tara diyorum

            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
            {
                Id = 1,
                ProductId = 1,
                Color = "Kırmızı",
                Width = 200,
                Height = 100
            },
            new ProductFeature()
            {
                Id = 2,
                ProductId = 2,
                Color = "Mavi",
                Width = 500,
                Height = 300
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
