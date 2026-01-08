using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.DataLayer.Repositories
{
    public interface IProductMaterialRep
    {
        public Task<ListResultObject<ProductMaterial>> GetAllProductMaterialsAsync(int ProductId = 0, int MaterialId = 0, int creatorId =0, int pageIndex = 1, int pageSize = 20, string searchText = "",string sortQuery ="");
        public Task<RowResultObject<ProductMaterial>> GetProductMaterialByIdAsync(int ProductMaterialId);
        public Task<BitResultObject> AddProductMaterialAsync(ProductMaterial ProductMaterial);
        public Task<BitResultObject> EditProductMaterialAsync(ProductMaterial ProductMaterial);
        public Task<BitResultObject> RemoveProductMaterialAsync(ProductMaterial ProductMaterial);
        public Task<BitResultObject> RemoveProductMaterialAsync(int ProductMaterialId);
        public Task<BitResultObject> ExistProductMaterialAsync(int ProductMaterialId);
    }
}
