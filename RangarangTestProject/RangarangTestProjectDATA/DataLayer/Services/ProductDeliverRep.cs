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
    public class ProductDeliverRep : IProductDeliverRep
    {
        private TheDbContext _context;

        public ProductDeliverRep(TheDbContext context)
        {
            _context = context;
        }

        public async Task<BitResultObject> AddProductDeliverAsync(ProductDeliver ProductDeliver)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                await _context.ProductDelivers.AddAsync(ProductDeliver);
                await _context.SaveChangesAsync();
                result.ID = ProductDeliver.ID;
                _context.Entry(ProductDeliver).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> EditProductDeliverAsync(ProductDeliver ProductDeliver)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductDelivers.Update(ProductDeliver);
                await _context.SaveChangesAsync();
                result.ID = ProductDeliver.ID;
                _context.Entry(ProductDeliver).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> ExistProductDeliverAsync(int ProductDeliverId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                result.Status = await _context.ProductDelivers
                .AsNoTracking()
                .AnyAsync(x => x.ID == ProductDeliverId);
                result.ID = ProductDeliverId;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     

        public async Task<ListResultObject<ProductDeliver>> GetAllProductDeliversAsync(int ProductId = 0, int CreatorId = 0, int pageIndex = 1, int pageSize = 20, string searchText = "", string sortQuery = "")
        {
            ListResultObject<ProductDeliver> results = new ListResultObject<ProductDeliver>();
            try
            {
                var query = _context.ProductDelivers.AsNoTracking();

                if (CreatorId > 0)
                {
                    query = query.Where(x => x.CreatorId == CreatorId);
                }

                if (ProductId > 0)
                {
                    query = query.Where(x => x.ProductId == ProductId);
                }
                
                query = query.Where(x =>
                    (!string.IsNullOrEmpty(x.Name.ToString()) && x.Name.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.StartCirculation.ToString()) && x.StartCirculation.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.EndCirculation.ToString()) && x.EndCirculation.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.Price.ToString()) && x.Price.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.PrintSide.ToString()) && x.PrintSide.ToString().Contains(searchText)) ||
                    (!string.IsNullOrEmpty(x.CalcType.ToString()) && x.CalcType.ToString().Contains(searchText)) ||
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

        public async Task<RowResultObject<ProductDeliver>> GetProductDeliverByIdAsync(int ProductDeliverId)
        {
            RowResultObject<ProductDeliver> result = new RowResultObject<ProductDeliver>();
            try
            {
                result.Result = await _context.ProductDelivers
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.ID == ProductDeliverId);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

     
        public async Task<BitResultObject> RemoveProductDeliverAsync(ProductDeliver ProductDeliver)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                _context.ProductDelivers.Remove(ProductDeliver);
                await _context.SaveChangesAsync();
                result.ID = ProductDeliver.ID;
                _context.Entry(ProductDeliver).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message} - {ex.InnerException?.Message}";
            }
            return result;
        }

        public async Task<BitResultObject> RemoveProductDeliverAsync(int ProductDeliverId)
        {
            BitResultObject result = new BitResultObject();
            try
            {
                var ProductDeliver = await GetProductDeliverByIdAsync(ProductDeliverId);
                result = await RemoveProductDeliverAsync(ProductDeliver.Result);
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