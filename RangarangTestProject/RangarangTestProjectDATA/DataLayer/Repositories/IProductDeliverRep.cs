using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.DataLayer.Repositories
{
    public interface IProductDeliverRep
    {
        public Task<ListResultObject<ProductDeliver>> GetAllProductDeliversAsync(int ProductId =0, int creatorId =0, int pageIndex = 1, int pageSize = 20, string searchText = "",string sortQuery ="");
        public Task<RowResultObject<ProductDeliver>> GetProductDeliverByIdAsync(int ProductDeliverId);
        public Task<BitResultObject> AddProductDeliverAsync(ProductDeliver ProductDeliver);
        public Task<BitResultObject> EditProductDeliverAsync(ProductDeliver ProductDeliver);
        public Task<BitResultObject> RemoveProductDeliverAsync(ProductDeliver ProductDeliver);
        public Task<BitResultObject> RemoveProductDeliverAsync(int ProductDeliverId);
        public Task<BitResultObject> ExistProductDeliverAsync(int ProductDeliverId);
    }
}
