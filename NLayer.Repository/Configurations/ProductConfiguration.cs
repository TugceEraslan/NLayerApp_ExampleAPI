using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();

            //  ################.##  (16 karakter noktadan önce olacak + 2 karakter noktadan sonra olacak = decimal(18,2) bu demek)
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");  // 18 karakterli parasal değer girilecek ve virgülden sonra 2 karakter kuruşu temsil edecek.
            builder.ToTable("Products");  // AppDbContext.cs de DbSet<> de tanımlanan isim tablo olarak defaultta gelir.

            /* builder product'ı temsil ettiğinden builder.HasOne(x => x.Category) ile her product'ın bir kategorisi olduğunu
             * devamında ki WithMany(x => x.Products) ile ama her kategorinin birden çok product'ı olacağını
             * devamında HasForeignKey(x => x.CategoryId) ile bu FK'nın CategoryId ile sağlandığını yazabiliriz */
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
        }
    }
}
