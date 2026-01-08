
using RangarangTestProjectDATA.Domain;

namespace RangarangTestProjectAPI.ViewModels
{

    public class ProductAdtPriceVM : BaseEntity
    {
        public int ProductAdtId { get; set; }
        public int ProductPriceId { get; set; }
        public int ProductAdtTypeId { get; set; }

        public float Price { get; set; }

    }

}
