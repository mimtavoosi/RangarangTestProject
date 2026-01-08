using RangarangTestProjectDATA.Domain;

namespace RangarangTestProjectAPI.ViewModels
{
    // User: جدول کاربران
    public class UserVM : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NationalCode { get; set; }
        public string Username { get; set; }
    }
}