using RangarangTestProjectAPI.Models.Public;

namespace RangarangTestProjectAPI.Models.Product
{
    public class GetProductListRequestBody:GetListRequestBody
    {
        public int ProductGroupId { get; set; } = 0;
        public int WorkTypeId { get; set; } = 0;
        public int SheetDimensionId { get; set; } = 0;
    }
}
