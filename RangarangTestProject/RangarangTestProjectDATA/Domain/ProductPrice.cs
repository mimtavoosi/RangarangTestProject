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

    public class ProductPrice : BaseEntity
    {
        public float Price { get; set; }

         public int Circulation { get; set; }

        public bool IsDoubleSided { get; set; }

        public int? PageCount { get; set; }
        public int? CopyCount { get; set; }

         public int ProductSizeId { get; set; }
         public int ProductMaterialId { get; set; }
        public int? ProductMaterialAttributeId { get; set; }
         public int ProductPrintKindId { get; set; }

        public bool IsJeld { get; set; } = false;

        [ForeignKey(nameof(ProductSizeId))]
        public ProductSize ProductSize { get; set; }

        [ForeignKey(nameof(ProductMaterialId))]
        public ProductMaterial ProductMaterial { get; set; }

        [ForeignKey(nameof(ProductMaterialAttributeId))]
        public ProductMaterialAttribute ProductMaterialAttribute { get; set; }

        [ForeignKey(nameof(ProductPrintKindId))]
        public ProductPrintKind ProductPrintKind { get; set; }
    }

}
