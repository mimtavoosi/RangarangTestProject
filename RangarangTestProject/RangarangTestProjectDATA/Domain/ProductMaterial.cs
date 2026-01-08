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
    public class ProductMaterial : BaseEntity
    {
         public int ProductId { get; set; }
         public int MaterialId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public bool IsJeld { get; set; } = false;
        public bool Required { get; set; } = false;
        public bool IsCustomCirculation { get; set; } = false;
        public bool IsCombinedMaterial { get; set; } = false;

        public int? Weight { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public ICollection<ProductMaterialAttribute> Attributes { get; set; }
        public ICollection<ProductPrice> Prices { get; set; }
    }

}
