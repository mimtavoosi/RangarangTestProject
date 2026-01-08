using RangarangTestProjectAPI.Models.Public;

namespace RangarangTestProjectAPI.Models.ProductPrice
{
    public class GetProductPriceListRequestBody:GetListRequestBody
    {
        public int ProductSizeId { get; set; } = 0;
        public int ProductMaterialId { get; set; } = 0;
        public int? ProductMaterialAttributeId { get; set; } = 0;
        public int ProductPrintKindId { get; set; } = 0;
    }
}
