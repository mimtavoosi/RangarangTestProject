using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.Domain
{
    public class Token:BaseEntity
    {
        public string TokenValue { get; set; } // مقدار توکن
        public string Type { get; set; } // رفرش توکن
        public bool Status { get; set; } // رفرش توکن
        public DateTime ExpiryDate { get; set; } // تاریخ انقضا
        public DateTime? RevokedDate { get; set; } // تاریخ لغو
    }
}