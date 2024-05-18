using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } /* Category'nin birden fazla products'ı ICollection<Product> Products
                                                            ile artık olabilecek. Entityler içerisindeki farklı class'lara referans
                                                             verdiğimiz propertylere navigation property denir.*/
    }
}
