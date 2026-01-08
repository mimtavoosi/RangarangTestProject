using RangarangTestProjectAPI.Models.Public;

namespace RangarangTestProjectAPI.Models.ProductAdt
{
    public class GetProductAdtListRequestBody:GetListRequestBody
    {
        public int ProductId { get; set; } = 0;
        public int AdtId { get; set; } = 0;
    }
}
