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
    public class ProductSize : BaseEntity
    {
         public int ProductId { get; set; }

        public float Length { get; set; }
        public float Width { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public int? SheetCount { get; set; }
        public int? SheetDimensionId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public ICollection<ProductDeliverSize> DeliverSizes { get; set; }
        public ICollection<ProductPrice> Prices { get; set; }
    }

}
