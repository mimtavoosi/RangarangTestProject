using RangarangTestProjectAPI.Models.Public;

namespace RangarangTestProjectAPI.Models.ProductPrintKind
{
    public class GetProductPrintKindListRequestBody : GetListRequestBody
    {
        public int ProductId { get; set; } = 0;
        public int PrintKindId { get; set; } = 0;
    }
}
