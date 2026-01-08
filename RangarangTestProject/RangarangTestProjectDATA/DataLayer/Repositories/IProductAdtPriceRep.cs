using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.DataLayer.Repositories
{
    public interface IProductAdtPriceRep
    {
        public Task<ListResultObject<ProductAdtPrice>> GetAllProductAdtPricesAsync(int ProductAdtId = 0, int ProductPriceId = 0, int ProductAdtTypeId = 0, int creatorId =0, int pageIndex = 1, int pageSize = 20, string searchText = "",string sortQuery ="");
        public Task<RowResultObject<ProductAdtPrice>> GetProductAdtPriceByIdAsync(int ProductAdtPriceId);
        public Task<BitResultObject> AddProductAdtPriceAsync(ProductAdtPrice ProductAdtPrice);
        public Task<BitResultObject> EditProductAdtPriceAsync(ProductAdtPrice ProductAdtPrice);
        public Task<BitResultObject> RemoveProductAdtPriceAsync(ProductAdtPrice ProductAdtPrice);
        public Task<BitResultObject> RemoveProductAdtPriceAsync(int ProductAdtPriceId);
        public Task<BitResultObject> ExistProductAdtPriceAsync(int ProductAdtPriceId);
    }
}
