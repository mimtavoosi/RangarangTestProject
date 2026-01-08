using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectAPI.Models.ProductSize
{
    public class AddEditProductSizeRequestBody
    {
        public int ID { get; set; } = 0;

        [Display(Name = "کد محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ProductId { get; set; }

        public float Length { get; set; }
        public float Width { get; set; }


        [Display(Name = "عنوان اندازه محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        public int? SheetCount { get; set; }
        public int? SheetDimensionId { get; set; }


    }
}
