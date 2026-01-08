using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.DataLayer.Repositories
{
    public interface IProductRep
    {
        public Task<ListResultObject<Product>> GetAllProductsAsync(int ProductGroupId = 0, int WorkTypeId = 0,int SheetDimensionId = 0, int creatorId =0, int pageIndex = 1, int pageSize = 20, string searchText = "",string sortQuery ="");
        public Task<RowResultObject<Product>> GetProductByIdAsync(int ProductId);
        public Task<BitResultObject> AddProductAsync(Product Product);
        public Task<BitResultObject> EditProductAsync(Product Product);
        public Task<BitResultObject> RemoveProductAsync(Product Product);
        public Task<BitResultObject> RemoveProductAsync(int ProductId);
        public Task<BitResultObject> ExistProductAsync(int ProductId);
    }
}
