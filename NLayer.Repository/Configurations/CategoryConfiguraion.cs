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
    public class CategoryConfiguraion : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
           builder.HasKey(x => x.Id);
           builder.Property(x => x.Id).UseIdentityColumn();  // UseIdentityColumn() default olarak birer birer artar 
           builder.Property(x => x.Name).IsRequired().HasMaxLength(50);  // IsRequired() db de null olmasın zorunlu olsun. HasMaxLength(50); Uzunluğuda max 50 karakter olsun.

           builder.ToTable("Categories");  // AppDbContext.cs de DbSet<> de tanımlanan isim tablo olarak defaultta gelir.
        }
    }
}
