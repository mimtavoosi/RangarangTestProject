using RangarangTestProjectDATA.DataLayer.Repositories;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using RangarangTestProjectDATA.Tools;
using RangarangTestProjectAPI.Models;
using RangarangTestProjectAPI.Models.Public;
using RangarangTestProjectAPI.Models.ProductSize;
using RangarangTestProjectAPI.Validations;
using RangarangTestProjectAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RangarangTestProjectAPI.Controllers
{
    [Route("ProductSize")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]


    public class ProductSizeController : ControllerBase
    {
        IProductSizeRep _ProductSizeRep;
        ILogRep _logRep;
        private readonly IMapper _mapper;


        public ProductSizeController(IProductSizeRep ProductSizeRep,ILogRep logRep,IMapper mapper)
        {
           _ProductSizeRep = ProductSizeRep;
           _logRep = logRep;
            _mapper = mapper;   
        }

        [HttpGet("GetAllProductSizes_Base")]
        public async Task<ActionResult<ListResultObject<ProductSizeVM>>> GetAllProductSizes_Base([FromQuery] GetProductSizeListRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductSizeRep.GetAllProductSizesAsync(requestBody.ProductId,requestBody.SheetDimensionId,requestBody.CreatorId,requestBody.PageIndex,requestBody.PageSize,requestBody.SearchText,requestBody.SortQuery);
            if (result.Status)
            {
                var resultVM = _mapper.Map<ListResultObject<ProductSizeVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [HttpGet("GetProductSizeById_Base")]
        public async Task<ActionResult<RowResultObject<ProductSizeVM>>> GetProductSizeById_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductSizeRep.GetProductSizeByIdAsync(requestBody.ID);
            if (result.Status)
            {
                var resultVM = _mapper.Map<RowResultObject<ProductSizeVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("ExistProductSize_Base")]
        public async Task<ActionResult<BitResultObject>> ExistProductSize_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductSizeRep.ExistProductSizeAsync(requestBody.ID);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddProductSize_Base")]
        public async Task<ActionResult<BitResultObject>> AddProductSize_Base(AddEditProductSizeRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            ProductSize ProductSize = new ProductSize()
            {
                CreateDate = DateTime.Now.ToShamsi(),
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                SheetDimensionId = requestBody.SheetDimensionId,
                ProductId = requestBody.ProductId,
                Length = requestBody.Length,
                Name = requestBody.Name,
                Width = requestBody.Width,
                SheetCount = requestBody.SheetCount,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),
                
            };
            var result = await _ProductSizeRep.AddProductSizeAsync(ProductSize);
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

        [HttpPut("EditProductSize_Base")]
        public async Task<ActionResult<BitResultObject>> EditProductSize_Base(AddEditProductSizeRequestBody requestBody)
        {
            var result = new BitResultObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var theRow = await _ProductSizeRep.GetProductSizeByIdAsync(requestBody.ID);
            if (!theRow.Status)
            {
                result.Status = theRow.Status;
                result.ErrorMessage = theRow.ErrorMessage;
            }

            ProductSize ProductSize = new ProductSize()
            {
                CreateDate = theRow.Result.CreateDate,
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                SheetDimensionId = requestBody.SheetDimensionId,
                ProductId = requestBody.ProductId,
                Length = requestBody.Length,
                Name = requestBody.Name,
                Width = requestBody.Width,
                SheetCount = requestBody.SheetCount,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),

            };
            result = await _ProductSizeRep.EditProductSizeAsync(ProductSize);
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

        [HttpDelete("DeleteProductSize_Base")]
        public async Task<ActionResult<BitResultObject>> DeleteProductSize_Base(GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductSizeRep.RemoveProductSizeAsync(requestBody.ID);
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
