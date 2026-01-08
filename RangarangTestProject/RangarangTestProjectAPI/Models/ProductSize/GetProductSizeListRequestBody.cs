using RangarangTestProjectAPI.Models.Public;

namespace RangarangTestProjectAPI.Models.ProductSize
{
    public class GetProductSizeListRequestBody : GetListRequestBody
    {
        public int ProductId { get; set; } = 0;
        public int? SheetDimensionId { get; set; } = 0;
    }
}
