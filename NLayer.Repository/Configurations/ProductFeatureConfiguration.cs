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
    public class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
           builder.HasKey(x => x.Id);
           builder.Property(x => x.Id).UseIdentityColumn();

            /* builder ProductFeature'a karşılık geliyor. Her ProductFeature'ın bir tane Product'ı oalabilir. builder.HasOne(x => x.Product)
             * Şimdi Product'tayım. Product ın da bir tane ProductFeature olabilir o yüzden WithOne(x => x.ProductFeature)
             * Şimdi ProductFeature'dayım.Birebir ilişkisinde ProductId'in FK olduğunu HasForeignKey<ProductFeature>(x => x.ProductId)
             * ile gösterdim*/

            builder.HasOne(x => x.Product).WithOne(x => x.ProductFeature).HasForeignKey<ProductFeature>(x => x.ProductId);
        }
    }
}
