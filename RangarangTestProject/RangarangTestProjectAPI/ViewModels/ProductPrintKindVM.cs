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
    public class ProductPrintKindVM : BaseEntity
    {
         public int ProductId { get; set; }
         public int PrintKindId { get; set; }

        public bool IsJeld { get; set; }
    }

}
