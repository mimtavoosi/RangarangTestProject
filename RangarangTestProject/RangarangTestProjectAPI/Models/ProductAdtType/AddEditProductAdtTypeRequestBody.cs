using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectAPI.Models.ProductAdtType
{
    public class AddEditProductAdtTypeRequestBody
    {
        public int ID { get; set; } = 0;

        [Display(Name = "کد افزونه محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ProductAdtId { get; set; }

        [Display(Name = "کد نوع افزونه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int AdtTypeId { get; set; }


    }
}
