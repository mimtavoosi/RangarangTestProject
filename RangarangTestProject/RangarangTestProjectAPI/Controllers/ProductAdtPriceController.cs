using RangarangTestProjectDATA.DataLayer.Repositories;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using RangarangTestProjectDATA.Tools;
using RangarangTestProjectAPI.Models;
using RangarangTestProjectAPI.Models.Public;
using RangarangTestProjectAPI.Models.ProductAdtPrice;
using RangarangTestProjectAPI.Validations;
using RangarangTestProjectAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RangarangTestProjectAPI.Controllers
{
    [Route("ProductAdtPrice")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]


    public class ProductAdtPriceController : ControllerBase
    {
        IProductAdtPriceRep _ProductAdtPriceRep;
        ILogRep _logRep;
        private readonly IMapper _mapper;


        public ProductAdtPriceController(IProductAdtPriceRep ProductAdtPriceRep,ILogRep logRep,IMapper mapper)
        {
           _ProductAdtPriceRep = ProductAdtPriceRep;
           _logRep = logRep;
            _mapper = mapper;   
        }

        [HttpGet("GetAllProductAdtPrices_Base")]
        public async Task<ActionResult<ListResultObject<ProductAdtPriceVM>>> GetAllProductAdtPrices_Base([FromQuery] GetProductAdtPriceListRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductAdtPriceRep.GetAllProductAdtPricesAsync(requestBody.ProductAdtId,requestBody.ProductPriceId,requestBody.ProductAdtTypeId,requestBody.CreatorId,requestBody.PageIndex,requestBody.PageSize,requestBody.SearchText,requestBody.SortQuery);
            if (result.Status)
            {
                var resultVM = _mapper.Map<ListResultObject<ProductAdtPriceVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [HttpGet("GetProductAdtPriceById_Base")]
        public async Task<ActionResult<RowResultObject<ProductAdtPriceVM>>> GetProductAdtPriceById_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductAdtPriceRep.GetProductAdtPriceByIdAsync(requestBody.ID);
            if (result.Status)
            {
                var resultVM = _mapper.Map<RowResultObject<ProductAdtPriceVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("ExistProductAdtPrice_Base")]
        public async Task<ActionResult<BitResultObject>> ExistProductAdtPrice_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductAdtPriceRep.ExistProductAdtPriceAsync(requestBody.ID);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddProductAdtPrice_Base")]
        public async Task<ActionResult<BitResultObject>> AddProductAdtPrice_Base(AddEditProductAdtPriceRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            ProductAdtPrice ProductAdtPrice = new ProductAdtPrice()
            {
                CreateDate = DateTime.Now.ToShamsi(),
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                ProductPriceId = requestBody.ProductPriceId,
                ProductAdtId = requestBody.ProductAdtId,
                ProductAdtTypeId = requestBody.ProductAdtTypeId,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),
                
            };
            var result = await _ProductAdtPriceRep.AddProductAdtPriceAsync(ProductAdtPrice);
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

        [HttpPut("EditProductAdtPrice_Base")]
        public async Task<ActionResult<BitResultObject>> EditProductAdtPrice_Base(AddEditProductAdtPriceRequestBody requestBody)
        {
            var result = new BitResultObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var theRow = await _ProductAdtPriceRep.GetProductAdtPriceByIdAsync(requestBody.ID);
            if (!theRow.Status)
            {
                result.Status = theRow.Status;
                result.ErrorMessage = theRow.ErrorMessage;
            }

            ProductAdtPrice ProductAdtPrice = new ProductAdtPrice()
            {
                CreateDate = theRow.Result.CreateDate,
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                ProductPriceId = requestBody.ProductPriceId,
                ProductAdtId = requestBody.ProductAdtId,
                ProductAdtTypeId = requestBody.ProductAdtTypeId,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),

            };
            result = await _ProductAdtPriceRep.EditProductAdtPriceAsync(ProductAdtPrice);
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

        [HttpDelete("DeleteProductAdtPrice_Base")]
        public async Task<ActionResult<BitResultObject>> DeleteProductAdtPrice_Base(GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductAdtPriceRep.RemoveProductAdtPriceAsync(requestBody.ID);
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
