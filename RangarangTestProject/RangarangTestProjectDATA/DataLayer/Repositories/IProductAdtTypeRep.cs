using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.DataLayer.Repositories
{
    public interface IProductAdtTypeRep
    {
        public Task<ListResultObject<ProductAdtType>> GetAllProductAdtTypesAsync(int ProductAdtId=0, int AdtTypeId = 0, int creatorId =0, int pageIndex = 1, int pageSize = 20, string searchText = "",string sortQuery ="");
        public Task<RowResultObject<ProductAdtType>> GetProductAdtTypeByIdAsync(int ProductAdtTypeId);
        public Task<BitResultObject> AddProductAdtTypeAsync(ProductAdtType ProductAdtType);
        public Task<BitResultObject> EditProductAdtTypeAsync(ProductAdtType ProductAdtType);
        public Task<BitResultObject> RemoveProductAdtTypeAsync(ProductAdtType ProductAdtType);
        public Task<BitResultObject> RemoveProductAdtTypeAsync(int ProductAdtTypeId);
        public Task<BitResultObject> ExistProductAdtTypeAsync(int ProductAdtTypeId);
    }
}
