using RangarangTestProjectAPI.Models.Public;

namespace RangarangTestProjectAPI.Models.ProductAdtType
{
    public class GetProductAdtTypeListRequestBody : GetListRequestBody
    {
        public int ProductAdtId { get; set; } = 0;
        public int AdtTypeId { get; set; } = 0;
    }
}
