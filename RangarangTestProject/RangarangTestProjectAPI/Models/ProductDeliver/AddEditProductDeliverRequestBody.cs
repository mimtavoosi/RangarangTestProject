using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectAPI.Models.ProductDeliver
{
    public class AddEditProductDeliverRequestBody
    {
        public int ID { get; set; } = 0;

        [Display(Name = "کد محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ProductId { get; set; }

        [Display(Name = "نام تحویل محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        public bool IsIncreased { get; set; } = true;

        public int StartCirculation { get; set; }
        public int EndCirculation { get; set; }

        public byte PrintSide { get; set; }
        public float Price { get; set; }
        public byte CalcType { get; set; }

    }
}
