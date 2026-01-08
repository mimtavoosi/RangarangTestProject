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
    public class ProductSizeRep : IProductSizeRep
    {
        private TheDbContext _context;

        public ProductSizeRep(TheDbContext context)
        {
            _context = context;
        }

        public async Task<BitResultObject> AddProductSizeAsync(ProductSize ProductSize)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                await _context.ProductSizes.AddAsync(ProductSize);
                await _context.SaveChangesAsync();
                result.ID = ProductSize.ID;
                _context.Entry(ProductSize).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> EditProductSizeAsync(ProductSize ProductSize)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductSizes.Update(ProductSize);
                await _context.SaveChangesAsync();
                result.ID = ProductSize.ID;
                _context.Entry(ProductSize).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> ExistProductSizeAsync(int ProductSizeId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                result.Status = await _context.ProductSizes
                .AsNoTracking()
                .AnyAsync(x => x.ID == ProductSizeId);
                result.ID = ProductSizeId;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     

        public async Task<ListResultObject<ProductSize>> GetAllProductSizesAsync(int ProductId = 0, int? SheetDimensionId = 0, int CreatorId = 0, int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "")
        {
            ListResultObject<ProductSize> results = new ListResultObject<ProductSize>();
            try
            {
                var query = _context.ProductSizes.AsNoTracking();

                if(ProductId > 0)
                {
                    query = query.Where(x => x.ProductId == ProductId);
                }

                if (SheetDimensionId > 0)
                {
                    query = query.Where(x => x.SheetDimensionId == SheetDimensionId);
                }

                if (CreatorId > 0)
                {
                    query = query.Where(x => x.CreatorId == CreatorId);
                }

                query = query.Where(x =>
                    (!string.IsNullOrEmpty(x.SheetCount.ToString()) && x.SheetCount.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.Width.ToString()) && x.Width.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.Length.ToString()) && x.Length.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.Name.ToString()) && x.Name.ToString().Contains(searchText)) ||
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

        public async Task<RowResultObject<ProductSize>> GetProductSizeByIdAsync(int ProductSizeId)
        {
            RowResultObject<ProductSize> result = new RowResultObject<ProductSize>();
            try
            {
                result.Result = await _context.ProductSizes
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == ProductSizeId);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveProductSizeAsync(ProductSize ProductSize)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductSizes.Remove(ProductSize);
                await _context.SaveChangesAsync();
                result.ID = ProductSize.ID;
                _context.Entry(ProductSize).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveProductSizeAsync(int ProductSizeId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                var ProductSize = await GetProductSizeByIdAsync(ProductSizeId);
                result = await RemoveProductSizeAsync(ProductSize.Result);
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