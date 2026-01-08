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
    public class ProductPrintKind : BaseEntity
    {
         public int ProductId { get; set; }
         public int PrintKindId { get; set; }

        public bool IsJeld { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public ICollection<ProductPrice> Prices { get; set; }
    }

}
