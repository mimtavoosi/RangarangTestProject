using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectAPI.Models.Authenticate
{
    public class ForgotPasswordRequestBody
    {
        [MaxLength(200)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "پست الکترونیک معتبر نیست")]
        [Display(Name = "پست الکترونیک")]
        public string Email { get; set; }
    }
}