using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangarangTestProjectDATA.DataLayer.Repositories
{
    public interface IProductDeliverSizeRep
    {
        public Task<ListResultObject<ProductDeliverSize>> GetAllProductDeliverSizesAsync(int ProductDeliverId = 0, int ProductSizeId =0,int creatorId =0, int pageIndex = 1, int pageSize = 20, string searchText = "",string sortQuery ="");
        public Task<RowResultObject<ProductDeliverSize>> GetProductDeliverSizeByIdAsync(int ProductDeliverSizeId);
        public Task<BitResultObject> AddProductDeliverSizeAsync(ProductDeliverSize ProductDeliverSize);
        public Task<BitResultObject> EditProductDeliverSizeAsync(ProductDeliverSize ProductDeliverSize);
        public Task<BitResultObject> RemoveProductDeliverSizeAsync(ProductDeliverSize ProductDeliverSize);
        public Task<BitResultObject> RemoveProductDeliverSizeAsync(int ProductDeliverSizeId);
        public Task<BitResultObject> ExistProductDeliverSizeAsync(int ProductDeliverSizeId);
    }
}
