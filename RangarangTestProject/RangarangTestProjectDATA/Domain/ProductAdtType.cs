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
    public class ProductAdtType : BaseEntity
    {
         public int ProductAdtId { get; set; }
         public int AdtTypeId { get; set; }

        [ForeignKey(nameof(ProductAdtId))]
        public ProductAdt ProductAdt { get; set; }

        public ICollection<ProductAdtPrice> Prices { get; set; }
    }

}
