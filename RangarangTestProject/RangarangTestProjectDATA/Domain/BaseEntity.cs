using RangarangTestProjectDATA.Tools;
using System.ComponentModel.DataAnnotations;

namespace RangarangTestProjectDATA.Domain
{
    public class BaseEntity
    {
        [Key]
        [Display(Name = "آیدی")]
        public int ID { get; set; }

        [Display(Name = "تاریخ ساخت")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "تاریخ بروزرسانی")]
        public DateTime? UpdateDate { get; set; } = DateTime.Now.ToShamsi();

        [Display(Name = "آیدی سازنده")]
        public int CreatorId { get; set; }

    }

}