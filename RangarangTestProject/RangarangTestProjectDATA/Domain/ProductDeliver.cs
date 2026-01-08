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

    public class ProductDeliver  : BaseEntity
    {
         public int ProductId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public bool IsIncreased { get; set; } = true;

        public int StartCirculation { get; set; }
        public int EndCirculation { get; set; }

        public byte PrintSide { get; set; }
        public float Price { get; set; }
        public byte CalcType { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public ICollection<ProductDeliverSize> Sizes { get; set; }
    }

}
