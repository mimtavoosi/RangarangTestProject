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
    public class ProductPrintKindRep : IProductPrintKindRep
    {
        private TheDbContext _context;

        public ProductPrintKindRep(TheDbContext context)
        {
            _context = context;
        }

        public async Task<BitResultObject> AddProductPrintKindAsync(ProductPrintKind ProductPrintKind)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                await _context.ProductPrintKinds.AddAsync(ProductPrintKind);
                await _context.SaveChangesAsync();
                result.ID = ProductPrintKind.ID;
                _context.Entry(ProductPrintKind).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> EditProductPrintKindAsync(ProductPrintKind ProductPrintKind)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductPrintKinds.Update(ProductPrintKind);
                await _context.SaveChangesAsync();
                result.ID = ProductPrintKind.ID;
                _context.Entry(ProductPrintKind).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> ExistProductPrintKindAsync(int ProductPrintKindId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                result.Status = await _context.ProductPrintKinds
                .AsNoTracking()
                .AnyAsync(x => x.ID == ProductPrintKindId);
                result.ID = ProductPrintKindId;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     

        public async Task<ListResultObject<ProductPrintKind>> GetAllProductPrintKindsAsync(int ProductId = 0, int PrintKindId = 0, int CreatorId = 0, int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "")
        {
            ListResultObject<ProductPrintKind> results = new ListResultObject<ProductPrintKind>();
            try
            {
                var query = _context.ProductPrintKinds.AsNoTracking();

                if (CreatorId > 0)
                {
                    query = query.Where(x => x.CreatorId == CreatorId);
                }

                if (ProductId > 0)
                {
                    query = query.Where(x => x.ProductId == ProductId);
                }

                if (PrintKindId > 0)
                {
                    query = query.Where(x => x.PrintKindId == PrintKindId);
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

        public async Task<RowResultObject<ProductPrintKind>> GetProductPrintKindByIdAsync(int ProductPrintKindId)
        {
            RowResultObject<ProductPrintKind> result = new RowResultObject<ProductPrintKind>();
            try
            {
                result.Result = await _context.ProductPrintKinds
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == ProductPrintKindId);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     
        public async Task<BitResultObject> RemoveProductPrintKindAsync(ProductPrintKind ProductPrintKind)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductPrintKinds.Remove(ProductPrintKind);
                await _context.SaveChangesAsync();
                result.ID = ProductPrintKind.ID;
                _context.Entry(ProductPrintKind).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveProductPrintKindAsync(int ProductPrintKindId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                var ProductPrintKind = await GetProductPrintKindByIdAsync(ProductPrintKindId);
                result = await RemoveProductPrintKindAsync(ProductPrintKind.Result);
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