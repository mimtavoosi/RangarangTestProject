
using RangarangTestProjectDATA.Domain;

namespace RangarangTestProjectAPI.ViewModels
{

    public class ProductAdtVM : BaseEntity
    {
        public int ProductId { get; set; }
        public int AdtId { get; set; }

        public bool Required { get; set; } = false;
        public byte? Side { get; set; }
        public int? Count { get; set; }
        public bool IsJeld { get; set; } = false;

    }

}
