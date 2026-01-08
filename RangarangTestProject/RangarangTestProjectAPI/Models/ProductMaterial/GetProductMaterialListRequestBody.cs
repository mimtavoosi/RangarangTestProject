using RangarangTestProjectAPI.Models.Public;

namespace RangarangTestProjectAPI.Models.ProductMaterial
{
    public class GetProductMaterialListRequestBody:GetListRequestBody
    {
        public int ProductId { get; set; } = 0;
        public int MaterialId { get; set; } = 0;
    }
}
