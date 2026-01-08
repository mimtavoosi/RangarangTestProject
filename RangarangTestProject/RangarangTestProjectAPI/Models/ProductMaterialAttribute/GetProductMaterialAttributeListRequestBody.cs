using RangarangTestProjectAPI.Models.Public;

namespace RangarangTestProjectAPI.Models.ProductMaterialAttribute
{
    public class GetProductMaterialAttributeListRequestBody : GetListRequestBody
    {
        public int ProductMaterialId { get; set; } = 0;
        public int MaterialAttributeId { get; set; } = 0;
    }
}
