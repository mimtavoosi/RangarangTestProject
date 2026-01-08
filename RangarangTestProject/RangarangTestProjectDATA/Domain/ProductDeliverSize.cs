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
    public class ProductDeliverSize : BaseEntity
    {
         public int ProductDeliverId { get; set; }
         public int ProductSizeId { get; set; }

        [ForeignKey(nameof(ProductDeliverId))]
        public ProductDeliver ProductDeliver { get; set; }

        [ForeignKey(nameof(ProductSizeId))]
        public ProductSize ProductSize { get; set; }
    }

}
