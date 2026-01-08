using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectAPI.Models.ProductPrintKind
{
    public class AddEditProductPrintKindRequestBody
    {
        public int ID { get; set; } = 0;

        [Display(Name = "کد محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ProductId { get; set; }

        [Display(Name = "کد نوع چاپ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int PrintKindId { get; set; }

        public bool IsJeld { get; set; }


    }
}
