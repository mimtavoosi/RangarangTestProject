using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.Domain
{
    [Index(nameof(ID), IsUnique = true)]
    public class ProductMaterialAttribute : BaseEntity
    {
         public int ProductMaterialId { get; set; }
         public int MaterialAttributeId { get; set; }

        [ForeignKey(nameof(ProductMaterialId))]
        public ProductMaterial ProductMaterial { get; set; }

        public ICollection<ProductPrice> Prices { get; set; }
    }

}
