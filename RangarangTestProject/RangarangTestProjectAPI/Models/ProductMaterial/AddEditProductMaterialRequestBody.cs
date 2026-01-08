using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectAPI.Models.ProductMaterial
{
    public class AddEditProductMaterialRequestBody
    {
        public int ID { get; set; } = 0;

        [Display(Name = "کد محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ProductId { get; set; }

        [Display(Name = "کد مواد اولیه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int MaterialId { get; set; }

        [Display(Name = "نامه ماده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]

        public string Name { get; set; }

        public bool IsJeld { get; set; } = false;
        public bool Required { get; set; } = false;
        public bool IsCustomCirculation { get; set; } = false;
        public bool IsCombinedMaterial { get; set; } = false;

        public int? Weight { get; set; }
    }
}
