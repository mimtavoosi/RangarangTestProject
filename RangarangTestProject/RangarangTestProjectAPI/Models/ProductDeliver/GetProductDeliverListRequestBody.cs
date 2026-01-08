using RangarangTestProjectAPI.Models.Public;

namespace RangarangTestProjectAPI.Models.ProductDeliver
{
    public class GetProductDeliverListRequestBody:GetListRequestBody
    {
        public int ProductId { get; set; } = 0;
    }
}
