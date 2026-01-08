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

    public class ProductAdt : BaseEntity
    {
         public int ProductId { get; set; }
         public int AdtId { get; set; }

        public bool Required { get; set; } = false;
        public byte? Side { get; set; }
        public int? Count { get; set; }
        public bool IsJeld { get; set; } = false;

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public ICollection<ProductAdtType> Types { get; set; }
        public ICollection<ProductAdtPrice> Prices { get; set; }
    }

}
