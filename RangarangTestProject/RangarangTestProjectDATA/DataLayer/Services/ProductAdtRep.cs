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
    public class ProductAdtRep : IProductAdtRep
    {
        private TheDbContext _context;

        public ProductAdtRep(TheDbContext context)
        {
            _context = context;
        }

        public async Task<BitResultObject> AddProductAdtAsync(ProductAdt ProductAdt)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                await _context.ProductAdts.AddAsync(ProductAdt);
                await _context.SaveChangesAsync();
                result.ID = ProductAdt.ID;
                _context.Entry(ProductAdt).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> EditProductAdtAsync(ProductAdt ProductAdt)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductAdts.Update(ProductAdt);
                await _context.SaveChangesAsync();
                result.ID = ProductAdt.ID;
                _context.Entry(ProductAdt).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> ExistProductAdtAsync(int ProductAdtId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                result.Status = await _context.ProductAdts
                .AsNoTracking()
                .AnyAsync(x => x.ID == ProductAdtId);
                result.ID = ProductAdtId;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     

        public async Task<ListResultObject<ProductAdt>> GetAllProductAdtsAsync(int ProductId = 0, int AdtId = 0, int CreatorId = 0, int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "")
        {
            ListResultObject<ProductAdt> results = new ListResultObject<ProductAdt>();
            try
            {
                var query = _context.ProductAdts.AsNoTracking();

                if (CreatorId > 0)
                {
                    query = query.Where(x => x.CreatorId == CreatorId);
                }

                if (ProductId > 0)
                {
                    query = query.Where(x => x.ProductId == ProductId);
                }

                if (AdtId > 0)
                {
                    query = query.Where(x => x.AdtId == AdtId);
                }

                
                query = query.Where(x =>
                    (!string.IsNullOrEmpty(x.Count.Value.ToString()) && x.Count.Value.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.Side.ToString()) && x.Side.ToString().Contains(searchText)) ||
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

        public async Task<RowResultObject<ProductAdt>> GetProductAdtByIdAsync(int ProductAdtId)
        {
            RowResultObject<ProductAdt> result = new RowResultObject<ProductAdt>();
            try
            {
                result.Result = await _context.ProductAdts
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == ProductAdtId);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     
        public async Task<BitResultObject> RemoveProductAdtAsync(ProductAdt ProductAdt)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductAdts.Remove(ProductAdt);
                await _context.SaveChangesAsync();
                result.ID = ProductAdt.ID;
                _context.Entry(ProductAdt).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveProductAdtAsync(int ProductAdtId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                var ProductAdt = await GetProductAdtByIdAsync(ProductAdtId);
                result = await RemoveProductAdtAsync(ProductAdt.Result);
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