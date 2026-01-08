using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectAPI.Models.ProductAdt
{
    public class AddEditProductAdtRequestBody
    {
        public int ID { get; set; } = 0;

        [Display(Name = "کد محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ProductId { get; set; }

        [Display(Name = "کد افزونه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int AdtId { get; set; }

        public bool Required { get; set; } = false;
        public byte? Side { get; set; }
        public int? Count { get; set; }
        public bool IsJeld { get; set; } = false;

    }
}
