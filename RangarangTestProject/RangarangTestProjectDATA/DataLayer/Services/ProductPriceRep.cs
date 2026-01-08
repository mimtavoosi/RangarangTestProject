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
    public class ProductPriceRep : IProductPriceRep
    {
        private TheDbContext _context;

        public ProductPriceRep(TheDbContext context)
        {
            _context = context;
        }

        public async Task<BitResultObject> AddProductPriceAsync(ProductPrice ProductPrice)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                await _context.ProductPrices.AddAsync(ProductPrice);
                await _context.SaveChangesAsync();
                result.ID = ProductPrice.ID;
                _context.Entry(ProductPrice).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> EditProductPriceAsync(ProductPrice ProductPrice)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductPrices.Update(ProductPrice);
                await _context.SaveChangesAsync();
                result.ID = ProductPrice.ID;
                _context.Entry(ProductPrice).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> ExistProductPriceAsync(int ProductPriceId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                result.Status = await _context.ProductPrices
                .AsNoTracking()
                .AnyAsync(x => x.ID == ProductPriceId);
                result.ID = ProductPriceId;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     

        public async Task<ListResultObject<ProductPrice>> GetAllProductPricesAsync(int ProductSizeId = 0, int ProductMaterialId = 0, int? ProductMaterialAttributeId = 0, int ProductPrintKindId = 0, int CreatorId = 0, int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "")
        {
            ListResultObject<ProductPrice> results = new ListResultObject<ProductPrice>();
            try
            {
                var query = _context.ProductPrices.AsNoTracking();

                if (CreatorId > 0)
                {
                    query = query.Where(x => x.CreatorId == CreatorId);
                }

                if (ProductSizeId > 0)
                {
                    query = query.Where(x => x.ProductSizeId == ProductSizeId);
                }

                if (ProductMaterialId > 0)
                {
                    query = query.Where(x => x.ProductMaterialId == ProductMaterialId);
                }

                if (ProductMaterialAttributeId > 0)
                {
                    query = query.Where(x => x.ProductMaterialAttributeId == ProductMaterialAttributeId);
                }

                if (ProductPrintKindId > 0)
                {
                    query = query.Where(x => x.ProductPrintKindId == ProductPrintKindId);
                }

                query = query.Where(x =>
                    (!string.IsNullOrEmpty(x.PageCount.Value.ToString()) && x.PageCount.Value.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.CopyCount.Value.ToString()) && x.CopyCount.Value.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.Circulation.ToString()) && x.Circulation.ToString().Contains(searchText)) ||
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

        public async Task<RowResultObject<ProductPrice>> GetProductPriceByIdAsync(int ProductPriceId)
        {
            RowResultObject<ProductPrice> result = new RowResultObject<ProductPrice>();
            try
            {
                result.Result = await _context.ProductPrices
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == ProductPriceId);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     
        public async Task<BitResultObject> RemoveProductPriceAsync(ProductPrice ProductPrice)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductPrices.Remove(ProductPrice);
                await _context.SaveChangesAsync();
                result.ID = ProductPrice.ID;
                _context.Entry(ProductPrice).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveProductPriceAsync(int ProductPriceId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                var ProductPrice = await GetProductPriceByIdAsync(ProductPriceId);
                result = await RemoveProductPriceAsync(ProductPrice.Result);
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