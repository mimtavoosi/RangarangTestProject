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
    public class ProductRep : IProductRep
    {
        private TheDbContext _context;

        public ProductRep(TheDbContext context)
        {
            _context = context;
        }

        public async Task<BitResultObject> AddProductAsync(Product Product)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                await _context.Products.AddAsync(Product);
                await _context.SaveChangesAsync();
                result.ID = Product.ID;
                _context.Entry(Product).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> EditProductAsync(Product Product)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.Products.Update(Product);
                await _context.SaveChangesAsync();
                result.ID = Product.ID;
                _context.Entry(Product).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> ExistProductAsync(int ProductId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                result.Status = await _context.Products
                .AsNoTracking()
                .AnyAsync(x => x.ID == ProductId);
                result.ID = ProductId;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     

        public async Task<ListResultObject<Product>> GetAllProductsAsync(int ProductGroupId = 0, int WorkTypeId = 0, int SheetDimensionId = 0, int CreatorId = 0, int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "")
        {
            ListResultObject<Product> results = new ListResultObject<Product>();
            try
            {
                var query = _context.Products.AsNoTracking();

                if (CreatorId > 0)
                {
                    query = query.Where(x => x.CreatorId == CreatorId);
                }

                if (ProductGroupId > 0)
                {
                    query = query.Where(x => x.ProductGroupId == ProductGroupId);
                }

                if (WorkTypeId > 0)
                {
                    query = query.Where(x => x.WorkTypeId == WorkTypeId);
                }

                if (SheetDimensionId > 0)
                {
                    query = query.Where(x => x.SheetDimensionId == SheetDimensionId);
                }

                
                query = query.Where(x =>
                    (!string.IsNullOrEmpty(x.MaxCirculation.Value.ToString()) && x.MaxCirculation.Value.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.MinCirculation.Value.ToString()) && x.MinCirculation.Value.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.MaxWidth.Value.ToString()) && x.MaxWidth.Value.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.MinWidth.Value.ToString()) && x.MinWidth.Value.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.MinLength.Value.ToString()) && x.MinLength.Value.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.MaxLength.Value.ToString()) && x.MaxLength.Value.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.MinPage.Value.ToString()) && x.MinPage.Value.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.MaxPage.Value.ToString()) && x.MaxPage.Value.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.MinCirculation.Value.ToString()) && x.MinCirculation.Value.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.MaxCirculation.Value.ToString()) && x.MaxCirculation.Value.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.PrintSide.ToString()) && x.PrintSide.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.CopyCount.ToString()) && x.CopyCount.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.PageCount.ToString()) && x.PageCount.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.CutMargin.ToString()) && x.CutMargin.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.PrintMargin.ToString()) && x.PrintMargin.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.ProductType.ToString()) && x.ProductType.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.Circulation.ToString()) && x.Circulation.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.FileExtension.ToString()) && x.FileExtension.ToString().Contains(searchText)) ||
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

        public async Task<RowResultObject<Product>> GetProductByIdAsync(int ProductId)
        {
            RowResultObject<Product> result = new RowResultObject<Product>();
            try
            {
                result.Result = await _context.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == ProductId);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     
        public async Task<BitResultObject> RemoveProductAsync(Product Product)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
                result.ID = Product.ID;
                _context.Entry(Product).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveProductAsync(int ProductId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                var Product = await GetProductByIdAsync(ProductId);
                result = await RemoveProductAsync(Product.Result);
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