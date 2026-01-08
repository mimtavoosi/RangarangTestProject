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
    public class ProductMaterialAttributeRep : IProductMaterialAttributeRep
    {
        private TheDbContext _context;

        public ProductMaterialAttributeRep(TheDbContext context)
        {
            _context = context;
        }

        public async Task<BitResultObject> AddProductMaterialAttributeAsync(ProductMaterialAttribute ProductMaterialAttribute)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                await _context.ProductMaterialAttributes.AddAsync(ProductMaterialAttribute);
                await _context.SaveChangesAsync();
                result.ID = ProductMaterialAttribute.ID;
                _context.Entry(ProductMaterialAttribute).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> EditProductMaterialAttributeAsync(ProductMaterialAttribute ProductMaterialAttribute)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductMaterialAttributes.Update(ProductMaterialAttribute);
                await _context.SaveChangesAsync();
                result.ID = ProductMaterialAttribute.ID;
                _context.Entry(ProductMaterialAttribute).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> ExistProductMaterialAttributeAsync(int ProductMaterialAttributeId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                result.Status = await _context.ProductMaterialAttributes
                .AsNoTracking()
                .AnyAsync(x => x.ID == ProductMaterialAttributeId);
                result.ID = ProductMaterialAttributeId;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     

        public async Task<ListResultObject<ProductMaterialAttribute>> GetAllProductMaterialAttributesAsync(int ProductMaterialId = 0, int MaterialAttributeId = 0, int CreatorId = 0, int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "")
        {
            ListResultObject<ProductMaterialAttribute> results = new ListResultObject<ProductMaterialAttribute>();
            try
            {
                var query = _context.ProductMaterialAttributes.AsNoTracking();

                if (CreatorId > 0)
                {
                    query = query.Where(x => x.CreatorId == CreatorId);
                }

                if (ProductMaterialId > 0)
                {
                    query = query.Where(x => x.ProductMaterialId == ProductMaterialId);
                }

                if (MaterialAttributeId > 0)
                {
                    query = query.Where(x => x.MaterialAttributeId == MaterialAttributeId);
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

        public async Task<RowResultObject<ProductMaterialAttribute>> GetProductMaterialAttributeByIdAsync(int ProductMaterialAttributeId)
        {
            RowResultObject<ProductMaterialAttribute> result = new RowResultObject<ProductMaterialAttribute>();
            try
            {
                result.Result = await _context.ProductMaterialAttributes
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == ProductMaterialAttributeId);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     
        public async Task<BitResultObject> RemoveProductMaterialAttributeAsync(ProductMaterialAttribute ProductMaterialAttribute)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductMaterialAttributes.Remove(ProductMaterialAttribute);
                await _context.SaveChangesAsync();
                result.ID = ProductMaterialAttribute.ID;
                _context.Entry(ProductMaterialAttribute).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveProductMaterialAttributeAsync(int ProductMaterialAttributeId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                var ProductMaterialAttribute = await GetProductMaterialAttributeByIdAsync(ProductMaterialAttributeId);
                result = await RemoveProductMaterialAttributeAsync(ProductMaterialAttribute.Result);
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