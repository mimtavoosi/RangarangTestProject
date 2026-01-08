using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.DataLayer.Repositories
{
    public interface IProductMaterialAttributeRep
    {
        public Task<ListResultObject<ProductMaterialAttribute>> GetAllProductMaterialAttributesAsync(int ProductMaterialId = 0, int MaterialAttributeId = 0, int creatorId =0, int pageIndex = 1, int pageSize = 20, string searchText = "",string sortQuery ="");
        public Task<RowResultObject<ProductMaterialAttribute>> GetProductMaterialAttributeByIdAsync(int ProductMaterialAttributeId);
        public Task<BitResultObject> AddProductMaterialAttributeAsync(ProductMaterialAttribute ProductMaterialAttribute);
        public Task<BitResultObject> EditProductMaterialAttributeAsync(ProductMaterialAttribute ProductMaterialAttribute);
        public Task<BitResultObject> RemoveProductMaterialAttributeAsync(ProductMaterialAttribute ProductMaterialAttribute);
        public Task<BitResultObject> RemoveProductMaterialAttributeAsync(int ProductMaterialAttributeId);
        public Task<BitResultObject> ExistProductMaterialAttributeAsync(int ProductMaterialAttributeId);
    }
}
