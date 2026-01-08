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
    public class ProductAdtPriceRep : IProductAdtPriceRep
    {
        private TheDbContext _context;

        public ProductAdtPriceRep(TheDbContext context)
        {
            _context = context;
        }

        public async Task<BitResultObject> AddProductAdtPriceAsync(ProductAdtPrice ProductAdtPrice)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                await _context.ProductAdtPrices.AddAsync(ProductAdtPrice);
                await _context.SaveChangesAsync();
                result.ID = ProductAdtPrice.ID;
                _context.Entry(ProductAdtPrice).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> EditProductAdtPriceAsync(ProductAdtPrice ProductAdtPrice)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductAdtPrices.Update(ProductAdtPrice);
                await _context.SaveChangesAsync();
                result.ID = ProductAdtPrice.ID;
                _context.Entry(ProductAdtPrice).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> ExistProductAdtPriceAsync(int ProductAdtPriceId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                result.Status = await _context.ProductAdtPrices
                .AsNoTracking()
                .AnyAsync(x => x.ID == ProductAdtPriceId);
                result.ID = ProductAdtPriceId;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     

        public async Task<ListResultObject<ProductAdtPrice>> GetAllProductAdtPricesAsync(int ProductAdtId = 0, int ProductPriceId = 0, int ProductAdtTypeId = 0, int CreatorId = 0, int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "")
        {
            ListResultObject<ProductAdtPrice> results = new ListResultObject<ProductAdtPrice>();
            try
            {
                var query = _context.ProductAdtPrices.AsNoTracking();

                if (CreatorId > 0)
                {
                    query = query.Where(x => x.CreatorId == CreatorId);
                }

                if (ProductAdtId > 0)
                {
                    query = query.Where(x => x.ProductAdtId == ProductAdtId);
                }

                if (ProductPriceId > 0)
                {
                    query = query.Where(x => x.ProductPriceId == ProductPriceId);
                }

                if (ProductAdtTypeId > 0)
                {
                    query = query.Where(x => x.ProductAdtTypeId == ProductAdtTypeId);
                }

                query = query.Where(x =>
                    (!string.IsNullOrEmpty(x.Price.ToString()) && x.Price.ToString().Contains(searchText)) ||
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

        public async Task<RowResultObject<ProductAdtPrice>> GetProductAdtPriceByIdAsync(int ProductAdtPriceId)
        {
            RowResultObject<ProductAdtPrice> result = new RowResultObject<ProductAdtPrice>();
            try
            {
                result.Result = await _context.ProductAdtPrices
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == ProductAdtPriceId);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     
        public async Task<BitResultObject> RemoveProductAdtPriceAsync(ProductAdtPrice ProductAdtPrice)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductAdtPrices.Remove(ProductAdtPrice);
                await _context.SaveChangesAsync();
                result.ID = ProductAdtPrice.ID;
                _context.Entry(ProductAdtPrice).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveProductAdtPriceAsync(int ProductAdtPriceId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                var ProductAdtPrice = await GetProductAdtPriceByIdAsync(ProductAdtPriceId);
                result = await RemoveProductAdtPriceAsync(ProductAdtPrice.Result);
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