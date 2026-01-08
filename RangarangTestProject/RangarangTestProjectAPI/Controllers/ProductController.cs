using RangarangTestProjectDATA.DataLayer.Repositories;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using RangarangTestProjectDATA.Tools;
using RangarangTestProjectAPI.Models;
using RangarangTestProjectAPI.Models.Public;
using RangarangTestProjectAPI.Models.Product;
using RangarangTestProjectAPI.Validations;
using RangarangTestProjectAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RangarangTestProjectAPI.Controllers
{
    [Route("Product")]
    [ApiController]
    //[Authorize]
    [Produces("application/json")]


    public class ProductController : ControllerBase
    {
        IProductRep _ProductRep;
        ILogRep _logRep;
        private readonly IMapper _mapper;


        public ProductController(IProductRep ProductRep,ILogRep logRep,IMapper mapper)
        {
           _ProductRep = ProductRep;
           _logRep = logRep;
            _mapper = mapper;   
        }

        [HttpGet("GetAllProducts_Base")]
        public async Task<ActionResult<ListResultObject<ProductVM>>> GetAllProducts_Base([FromQuery] GetProductListRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductRep.GetAllProductsAsync(requestBody.ProductGroupId,requestBody.WorkTypeId,requestBody.SheetDimensionId,requestBody.CreatorId,requestBody.PageIndex,requestBody.PageSize,requestBody.SearchText,requestBody.SortQuery);
            if (result.Status)
            {
                var resultVM = _mapper.Map<ListResultObject<ProductVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [HttpGet("GetProductById_Base")]
        public async Task<ActionResult<RowResultObject<ProductVM>>> GetProductById_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductRep.GetProductByIdAsync(requestBody.ID);
            if (result.Status)
            {
                var resultVM = _mapper.Map<RowResultObject<ProductVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("ExistProduct_Base")]
        public async Task<ActionResult<BitResultObject>> ExistProduct_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductRep.ExistProductAsync(requestBody.ID);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddProduct_Base")]
        public async Task<ActionResult<BitResultObject>> AddProduct_Base(AddEditProductRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            Product Product = new Product()
            {
                CreateDate = DateTime.Now.ToShamsi(),
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                ProductGroupId = requestBody.ProductGroupId,
                WorkTypeId = requestBody.WorkTypeId,
                SheetDimensionId = requestBody.SheetDimensionId,
                Circulation = requestBody.Circulation,
                CopyCount = requestBody.CopyCount,
                CutMargin = requestBody.CutMargin,
                FileExtension = requestBody.FileExtension,
                IsCalculatePrice = requestBody.IsCalculatePrice,
                IsCheckFile = requestBody.IsCheckFile,
                IsDelete = requestBody.IsDelete,
                IsCmyk = requestBody.IsCmyk,
                IsCustomCirculation = requestBody.IsCustomCirculation,
                IsCustomPage = requestBody.IsCustomPage,
                IsCustomSize = requestBody.IsCustomSize,
                MaxCirculation  = requestBody.MaxCirculation,
                MaxLength = requestBody.MaxLength,
                MaxPage = requestBody.MaxPage,
                MaxWidth = requestBody.MaxWidth,
                MinCirculation = requestBody.MinCirculation,
                MinLength = requestBody.MinLength,
                MinPage = requestBody.MinPage,
                MinWidth = requestBody.MinLength,
                PageCount = requestBody.PageCount,
                PrintMargin = requestBody.PrintMargin,
                PrintSide = requestBody.PrintSide,  
                ProductType = requestBody.ProductType,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),

            };
            var result = await _ProductRep.AddProductAsync(Product);
            if (result.Status)
            {
                #region AddLog

                Log log = new Log()
                {
                    CreateDate = DateTime.Now.ToShamsi(),
                    UpdateDate = DateTime.Now.ToShamsi(),
                    LogTime = DateTime.Now.ToShamsi(),
                    ActionName = this.ControllerContext.RouteData.Values["action"].ToString(),
                    CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),

                };
                await _logRep.AddLogAsync(log);

                #endregion


                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("EditProduct_Base")]
        public async Task<ActionResult<BitResultObject>> EditProduct_Base(AddEditProductRequestBody requestBody)
        {
            var result = new BitResultObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var theRow = await _ProductRep.GetProductByIdAsync(requestBody.ID);
            if (!theRow.Status)
            {
                result.Status = theRow.Status;
                result.ErrorMessage = theRow.ErrorMessage;
            }

            Product Product = new Product()
            {
                CreateDate = theRow.Result.CreateDate,
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                ProductGroupId = requestBody.ProductGroupId,
                WorkTypeId = requestBody.WorkTypeId,
                SheetDimensionId = requestBody.SheetDimensionId,
                Circulation = requestBody.Circulation,
                CopyCount = requestBody.CopyCount,
                CutMargin = requestBody.CutMargin,
                FileExtension = requestBody.FileExtension,
                IsCalculatePrice = requestBody.IsCalculatePrice,
                IsCheckFile = requestBody.IsCheckFile,
                IsDelete = requestBody.IsDelete,
                IsCmyk = requestBody.IsCmyk,
                IsCustomCirculation = requestBody.IsCustomCirculation,
                IsCustomPage = requestBody.IsCustomPage,
                IsCustomSize = requestBody.IsCustomSize,
                MaxCirculation = requestBody.MaxCirculation,
                MaxLength = requestBody.MaxLength,
                MaxPage = requestBody.MaxPage,
                MaxWidth = requestBody.MaxWidth,
                MinCirculation = requestBody.MinCirculation,
                MinLength = requestBody.MinLength,
                MinPage = requestBody.MinPage,
                MinWidth = requestBody.MinLength,
                PageCount = requestBody.PageCount,
                PrintMargin = requestBody.PrintMargin,
                PrintSide = requestBody.PrintSide,
                ProductType = requestBody.ProductType,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),

            };
            result = await _ProductRep.EditProductAsync(Product);
            if (result.Status)
            {

                #region AddLog

                Log log = new Log()
                {
                    CreateDate = DateTime.Now.ToShamsi(),
                    UpdateDate = DateTime.Now.ToShamsi(),
                    LogTime = DateTime.Now.ToShamsi(),
                    ActionName = this.ControllerContext.RouteData.Values["action"].ToString(),
                    CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),

                };
                await _logRep.AddLogAsync(log);

                #endregion

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteProduct_Base")]
        public async Task<ActionResult<BitResultObject>> DeleteProduct_Base(GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductRep.RemoveProductAsync(requestBody.ID);
            if (result.Status)
            {

                #region AddLog

                Log log = new Log()
                {
                    CreateDate = DateTime.Now.ToShamsi(),
                    UpdateDate = DateTime.Now.ToShamsi(),
                    LogTime = DateTime.Now.ToShamsi(),
                    ActionName = this.ControllerContext.RouteData.Values["action"].ToString(),
                    CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),

                };
                await _logRep.AddLogAsync(log);

                #endregion

                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
