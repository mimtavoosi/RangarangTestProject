using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectAPI.Models.Authenticate
{
    public class RefreshTokenRequestBody
    {
        [Display(Name = "رفرش توکن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string RefreshToken { get; set; }
    }
}
