using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectAPI.Models.ProductPrice
{
    public class AddEditProductPriceRequestBody
    {
        public int ID { get; set; } = 0;


        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public float Price { get; set; }

        public int Circulation { get; set; }

        public bool IsDoubleSided { get; set; }

        public int? PageCount { get; set; }
        public int? CopyCount { get; set; }

        [Display(Name = "کد اندازه محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ProductSizeId { get; set; }

        [Display(Name = "کد مواد محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ProductMaterialId { get; set; }

        public int? ProductMaterialAttributeId { get; set; }

        [Display(Name = "کد نوع چاپ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ProductPrintKindId { get; set; }

        public bool IsJeld { get; set; } = false;
    }
}
