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

    public class ProductAdtPrice : BaseEntity
    {
         public int ProductAdtId { get; set; }
         public int ProductPriceId { get; set; }
         public int ProductAdtTypeId { get; set; }

        public float Price { get; set; }

        [ForeignKey(nameof(ProductAdtId))]
        public ProductAdt ProductAdt { get; set; }

        [ForeignKey(nameof(ProductPriceId))]
        public ProductPrice ProductPrice { get; set; }

        [ForeignKey(nameof(ProductAdtTypeId))]
        public ProductAdtType ProductAdtType { get; set; }
    }

}
