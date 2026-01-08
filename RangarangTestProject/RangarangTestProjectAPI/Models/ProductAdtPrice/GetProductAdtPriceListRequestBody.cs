using RangarangTestProjectAPI.Models.Public;

namespace RangarangTestProjectAPI.Models.ProductAdtPrice
{
    public class GetProductAdtPriceListRequestBody : GetListRequestBody
    {
        public int ProductAdtId { get; set; } = 0;
        public int ProductPriceId { get; set; } = 0;
        public int ProductAdtTypeId { get; set; } = 0;
    }
}
