using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    /* AppDbContext.cs sınıfımızı kirletmemek için CategorySeed sınıfımız, IEntityTypeConfiguration<Category> sınıfından miras alıyor
     * Yoksa aynı işlemleri AppDbContext.cs sınıfı içinde de yapabilirdik */
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Id'yi sadece SeedData esnasında veriyorum.Uygulamamnın genelinde repository kullandığımızda category oluşturmak istediğimiz zaman
            // Id vermeme gerek yok
            builder.HasData(
                new Category() { Id = 1, Name = "Kalemler" }, 
                new Category() { Id = 2, Name = "Kitaplar" }, 
                new Category() { Id = 3, Name = "Defterler" });
        }
    }
}
