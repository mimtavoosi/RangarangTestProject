using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.DataLayer.Repositories
{
    public interface IProductPriceRep
    {
        public Task<ListResultObject<ProductPrice>> GetAllProductPricesAsync(int ProductSizeId = 0, int ProductMaterialId = 0, int? ProductMaterialAttributeId = 0, int ProductPrintKindId = 0, int creatorId =0, int pageIndex = 1, int pageSize = 20, string searchText = "",string sortQuery ="");
        public Task<RowResultObject<ProductPrice>> GetProductPriceByIdAsync(int ProductPriceId);
        public Task<BitResultObject> AddProductPriceAsync(ProductPrice ProductPrice);
        public Task<BitResultObject> EditProductPriceAsync(ProductPrice ProductPrice);
        public Task<BitResultObject> RemoveProductPriceAsync(ProductPrice ProductPrice);
        public Task<BitResultObject> RemoveProductPriceAsync(int ProductPriceId);
        public Task<BitResultObject> ExistProductPriceAsync(int ProductPriceId);
    }
}
