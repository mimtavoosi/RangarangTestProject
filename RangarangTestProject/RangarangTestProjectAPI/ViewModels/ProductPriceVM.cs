using Microsoft.EntityFrameworkCore;
using RangarangTestProjectDATA.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectAPI.ViewModels
{
    public class ProductPriceVM : BaseEntity
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

    }

}
