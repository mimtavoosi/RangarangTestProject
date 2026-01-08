using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.DataLayer.Repositories
{
    public interface IProductAdtRep
    {
        public Task<ListResultObject<ProductAdt>> GetAllProductAdtsAsync(int ProductId = 0, int AdtId = 0, int creatorId =0, int pageIndex = 1, int pageSize = 20, string searchText = "",string sortQuery ="");
        public Task<RowResultObject<ProductAdt>> GetProductAdtByIdAsync(int ProductAdtId);
        public Task<BitResultObject> AddProductAdtAsync(ProductAdt ProductAdt);
        public Task<BitResultObject> EditProductAdtAsync(ProductAdt ProductAdt);
        public Task<BitResultObject> RemoveProductAdtAsync(ProductAdt ProductAdt);
        public Task<BitResultObject> RemoveProductAdtAsync(int ProductAdtId);
        public Task<BitResultObject> ExistProductAdtAsync(int ProductAdtId);
    }
}
