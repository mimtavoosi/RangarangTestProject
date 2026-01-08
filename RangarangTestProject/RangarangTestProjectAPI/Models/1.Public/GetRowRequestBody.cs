using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectAPI.Models.Public
{
    public class GetRowRequestBody
    {
        [Display(Name = "کد ردیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "مقدار {0} باید بزرگتر از 0 باشد")]
        public int ID { get; set; }
    }
}
