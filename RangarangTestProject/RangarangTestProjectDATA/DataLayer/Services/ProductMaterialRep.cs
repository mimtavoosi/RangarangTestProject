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
    public class ProductMaterialRep : IProductMaterialRep
    {
        private TheDbContext _context;

        public ProductMaterialRep(TheDbContext context)
        {
            _context = context;
        }

        public async Task<BitResultObject> AddProductMaterialAsync(ProductMaterial ProductMaterial)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                await _context.ProductMaterials.AddAsync(ProductMaterial);
                await _context.SaveChangesAsync();
                result.ID = ProductMaterial.ID;
                _context.Entry(ProductMaterial).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> EditProductMaterialAsync(ProductMaterial ProductMaterial)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductMaterials.Update(ProductMaterial);
                await _context.SaveChangesAsync();
                result.ID = ProductMaterial.ID;
                _context.Entry(ProductMaterial).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> ExistProductMaterialAsync(int ProductMaterialId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                result.Status = await _context.ProductMaterials
                .AsNoTracking()
                .AnyAsync(x => x.ID == ProductMaterialId);
                result.ID = ProductMaterialId;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     

        public async Task<ListResultObject<ProductMaterial>> GetAllProductMaterialsAsync(int ProductId = 0, int MaterialId = 0, int CreatorId = 0, int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "")
        {
            ListResultObject<ProductMaterial> results = new ListResultObject<ProductMaterial>();
            try
            {
                var query = _context.ProductMaterials.AsNoTracking();

                if (CreatorId > 0)
                {
                    query = query.Where(x => x.CreatorId == CreatorId);
                }

                if (ProductId > 0)
                {
                    query = query.Where(x => x.ProductId == ProductId);
                }

                if (MaterialId > 0)
                {
                    query = query.Where(x => x.MaterialId == MaterialId);
                }

                query = query.Where(x =>
                    (!string.IsNullOrEmpty(x.Name.ToString()) && x.Name.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.Weight.Value.ToString()) && x.Weight.Value.ToString().Contains(searchText)) ||
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

        public async Task<RowResultObject<ProductMaterial>> GetProductMaterialByIdAsync(int ProductMaterialId)
        {
            RowResultObject<ProductMaterial> result = new RowResultObject<ProductMaterial>();
            try
            {
                result.Result = await _context.ProductMaterials
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == ProductMaterialId);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     
        public async Task<BitResultObject> RemoveProductMaterialAsync(ProductMaterial ProductMaterial)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductMaterials.Remove(ProductMaterial);
                await _context.SaveChangesAsync();
                result.ID = ProductMaterial.ID;
                _context.Entry(ProductMaterial).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveProductMaterialAsync(int ProductMaterialId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                var ProductMaterial = await GetProductMaterialByIdAsync(ProductMaterialId);
                result = await RemoveProductMaterialAsync(ProductMaterial.Result);
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