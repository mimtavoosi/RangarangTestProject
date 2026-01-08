using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.DataLayer.Repositories
{
    public interface IProductSizeRep
    {
        public Task<ListResultObject<ProductSize>> GetAllProductSizesAsync(int ProductId = 0, int? SheetDimensionId = 0, int creatorId =0, int pageIndex = 1, int pageSize = 20, string searchText = "",string sortQuery ="");
        public Task<RowResultObject<ProductSize>> GetProductSizeByIdAsync(int ProductSizeId);
        public Task<BitResultObject> AddProductSizeAsync(ProductSize ProductSize);
        public Task<BitResultObject> EditProductSizeAsync(ProductSize ProductSize);
        public Task<BitResultObject> RemoveProductSizeAsync(ProductSize ProductSize);
        public Task<BitResultObject> RemoveProductSizeAsync(int ProductSizeId);
        public Task<BitResultObject> ExistProductSizeAsync(int ProductSizeId);
    }
}
