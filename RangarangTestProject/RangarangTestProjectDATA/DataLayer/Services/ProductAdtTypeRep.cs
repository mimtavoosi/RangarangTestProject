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
    public class ProductAdtTypeRep : IProductAdtTypeRep
    {
        private TheDbContext _context;

        public ProductAdtTypeRep(TheDbContext context)
        {
            _context = context;
        }

        public async Task<BitResultObject> AddProductAdtTypeAsync(ProductAdtType ProductAdtType)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                await _context.ProductAdtTypes.AddAsync(ProductAdtType);
                await _context.SaveChangesAsync();
                result.ID = ProductAdtType.ID;
                _context.Entry(ProductAdtType).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> EditProductAdtTypeAsync(ProductAdtType ProductAdtType)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductAdtTypes.Update(ProductAdtType);
                await _context.SaveChangesAsync();
                result.ID = ProductAdtType.ID;
                _context.Entry(ProductAdtType).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> ExistProductAdtTypeAsync(int ProductAdtTypeId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                result.Status = await _context.ProductAdtTypes
                .AsNoTracking()
                .AnyAsync(x => x.ID == ProductAdtTypeId);
                result.ID = ProductAdtTypeId;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     

        public async Task<ListResultObject<ProductAdtType>> GetAllProductAdtTypesAsync(int ProductAdtId = 0, int AdtTypeId = 0, int CreatorId = 0, int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "")
        {
            ListResultObject<ProductAdtType> results = new ListResultObject<ProductAdtType>();
            try
            {
                var query = _context.ProductAdtTypes.AsNoTracking();

                if (CreatorId > 0)
                {
                    query = query.Where(x => x.CreatorId == CreatorId);
                }

                if (ProductAdtId > 0)
                {
                    query = query.Where(x => x.ProductAdtId == ProductAdtId);
                }

                if (AdtTypeId > 0)
                {
                    query = query.Where(x => x.AdtTypeId == AdtTypeId);
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

        public async Task<RowResultObject<ProductAdtType>> GetProductAdtTypeByIdAsync(int ProductAdtTypeId)
        {
            RowResultObject<ProductAdtType> result = new RowResultObject<ProductAdtType>();
            try
            {
                result.Result = await _context.ProductAdtTypes
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == ProductAdtTypeId);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     
        public async Task<BitResultObject> RemoveProductAdtTypeAsync(ProductAdtType ProductAdtType)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductAdtTypes.Remove(ProductAdtType);
                await _context.SaveChangesAsync();
                result.ID = ProductAdtType.ID;
                _context.Entry(ProductAdtType).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveProductAdtTypeAsync(int ProductAdtTypeId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                var ProductAdtType = await GetProductAdtTypeByIdAsync(ProductAdtTypeId);
                result = await RemoveProductAdtTypeAsync(ProductAdtType.Result);
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