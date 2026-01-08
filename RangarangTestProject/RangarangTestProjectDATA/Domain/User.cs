
using System.ComponentModel.DataAnnotations;
using RangarangTestProjectDATA.Domain;
using Microsoft.AspNetCore.Identity;

namespace RangarangTestProjectDATA.Domain
{
    // User: جدول کاربران
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NationalCode { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // هش رمز عبور
    }
}