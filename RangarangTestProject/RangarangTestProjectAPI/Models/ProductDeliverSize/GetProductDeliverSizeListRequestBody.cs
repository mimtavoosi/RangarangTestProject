using RangarangTestProjectAPI.Models.Public;

namespace RangarangTestProjectAPI.Models.ProductDeliverSize
{
    public class GetProductDeliverSizeListRequestBody : GetListRequestBody
    {
        public int ProductDeliverId { get; set; } = 0;
        public int ProductSizeId { get; set; } = 0;
    }
}
