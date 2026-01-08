using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.DataLayer;
using RangarangTestProjectDATA.ResultObjects;
using Azure.Core;
using RangarangTestProjectDATA.Tools;
using RangarangTestProjectDATA.DataLayer.Repositories;


namespace Services
{
    public class ProductDeliverSizeRep : IProductDeliverSizeRep
    {
        private TheDbContext _context;

        public ProductDeliverSizeRep(TheDbContext context)
        {
            _context = context;
        }

        public async Task<BitResultObject> AddProductDeliverSizeAsync(ProductDeliverSize ProductDeliverSize)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                await _context.ProductDeliverSizes.AddAsync(ProductDeliverSize);
                await _context.SaveChangesAsync();
                result.ID = ProductDeliverSize.ID;
                _context.Entry(ProductDeliverSize).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> EditProductDeliverSizeAsync(ProductDeliverSize ProductDeliverSize)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductDeliverSizes.Update(ProductDeliverSize);
                await _context.SaveChangesAsync();
                result.ID = ProductDeliverSize.ID;
                _context.Entry(ProductDeliverSize).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> ExistProductDeliverSizeAsync(int ProductDeliverSizeId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                result.Status = await _context.ProductDeliverSizes
                .AsNoTracking()
                .AnyAsync(x => x.ID == ProductDeliverSizeId);
                result.ID = ProductDeliverSizeId;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     

        public async Task<ListResultObject<ProductDeliverSize>> GetAllProductDeliverSizesAsync(int ProductDeliverId = 0, int ProductSizeId = 0, int CreatorId = 0, int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "")
        {
            ListResultObject<ProductDeliverSize> results = new ListResultObject<ProductDeliverSize>();
            try
            {
                var query = _context.ProductDeliverSizes.AsNoTracking();

                if (CreatorId > 0)
                {
                    query = query.Where(x => x.CreatorId == CreatorId);
                }

                if (ProductDeliverId > 0)
                {
                    query = query.Where(x => x.ProductDeliverId == ProductDeliverId);
                }

                if (ProductSizeId > 0)
                {
                    query = query.Where(x => x.ProductSizeId == ProductSizeId);
                }

                query = query.Where(x =>
                    (x.CreateDate.ToString().Contains(searchText)) ||
                    (x.UpdateDate.ToString().Contains(searchText))
                );

                results.TotalCount = query.Count();
                results.PageCount = DbTools.GetPageCount(results.TotalCount, pageSize);
                results.Results = await query.OrderByDescending(x => x.CreateDate)
                 .SortBy(sortQuery).ToPaging(pageIndex, pageSize)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                results.Status = false;
                results.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return results;
        }

        public async Task<RowResultObject<ProductDeliverSize>> GetProductDeliverSizeByIdAsync(int ProductDeliverSizeId)
        {
            RowResultObject<ProductDeliverSize> result = new RowResultObject<ProductDeliverSize>();
            try
            {
                result.Result = await _context.ProductDeliverSizes
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == ProductDeliverSizeId);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     
        public async Task<BitResultObject> RemoveProductDeliverSizeAsync(ProductDeliverSize ProductDeliverSize)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductDeliverSizes.Remove(ProductDeliverSize);
                await _context.SaveChangesAsync();
                result.ID = ProductDeliverSize.ID;
                _context.Entry(ProductDeliverSize).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveProductDeliverSizeAsync(int ProductDeliverSizeId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                var ProductDeliverSize = await GetProductDeliverSizeByIdAsync(ProductDeliverSizeId);
                result = await RemoveProductDeliverSizeAsync(ProductDeliverSize.Result);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }
    }
}