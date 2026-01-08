using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectAPI.Models.ProductDeliverSize
{
    public class AddEditProductDeliverSizeRequestBody
    {
        public int ID { get; set; } = 0;

        [Display(Name = "کد تحویل محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ProductDeliverId { get; set; }

        [Display(Name = "کد اندازه محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ProductSizeId { get; set; }


    }
}
