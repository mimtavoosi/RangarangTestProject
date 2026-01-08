using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectAPI.Models.ProductMaterialAttribute
{
    public class AddEditProductMaterialAttributeRequestBody
    {
        public int ID { get; set; } = 0;

        [Display(Name = "کد مواد اولیه محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ProductMaterialId { get; set; }

        [Display(Name = "کد خاصیت مواد اولیه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int MaterialAttributeId { get; set; }


    }
}
