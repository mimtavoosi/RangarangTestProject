using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.DataLayer.Repositories
{
    public interface IProductPrintKindRep
    {
        public Task<ListResultObject<ProductPrintKind>> GetAllProductPrintKindsAsync(int ProductId = 0, int PrintKindId = 0, int creatorId =0, int pageIndex = 1, int pageSize = 20, string searchText = "",string sortQuery ="");
        public Task<RowResultObject<ProductPrintKind>> GetProductPrintKindByIdAsync(int ProductPrintKindId);
        public Task<BitResultObject> AddProductPrintKindAsync(ProductPrintKind ProductPrintKind);
        public Task<BitResultObject> EditProductPrintKindAsync(ProductPrintKind ProductPrintKind);
        public Task<BitResultObject> RemoveProductPrintKindAsync(ProductPrintKind ProductPrintKind);
        public Task<BitResultObject> RemoveProductPrintKindAsync(int ProductPrintKindId);
        public Task<BitResultObject> ExistProductPrintKindAsync(int ProductPrintKindId);
    }
}
