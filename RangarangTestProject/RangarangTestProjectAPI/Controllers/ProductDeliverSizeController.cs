using RangarangTestProjectDATA.DataLayer.Repositories;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using RangarangTestProjectDATA.Tools;
using RangarangTestProjectAPI.Models;
using RangarangTestProjectAPI.Models.Public;
using RangarangTestProjectAPI.Models.ProductDeliverSize;
using RangarangTestProjectAPI.Validations;
using RangarangTestProjectAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RangarangTestProjectAPI.Controllers
{
    [Route("ProductDeliverSize")]
    [ApiController]
    //[Authorize]
    [Produces("application/json")]


    public class ProductDeliverSizeController : ControllerBase
    {
        IProductDeliverSizeRep _ProductDeliverSizeRep;
        ILogRep _logRep;
        private readonly IMapper _mapper;


        public ProductDeliverSizeController(IProductDeliverSizeRep ProductDeliverSizeRep,ILogRep logRep,IMapper mapper)
        {
           _ProductDeliverSizeRep = ProductDeliverSizeRep;
           _logRep = logRep;
            _mapper = mapper;   
        }

        [HttpGet("GetAllProductDeliverSizes_Base")]
        public async Task<ActionResult<ListResultObject<ProductDeliverSizeVM>>> GetAllProductDeliverSizes_Base([FromQuery] GetProductDeliverSizeListRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductDeliverSizeRep.GetAllProductDeliverSizesAsync(requestBody.ProductDeliverId,requestBody.ProductSizeId,requestBody.CreatorId,requestBody.PageIndex,requestBody.PageSize,requestBody.SearchText,requestBody.SortQuery);
            if (result.Status)
            {
                var resultVM = _mapper.Map<ListResultObject<ProductDeliverSizeVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [HttpGet("GetProductDeliverSizeById_Base")]
        public async Task<ActionResult<RowResultObject<ProductDeliverSizeVM>>> GetProductDeliverSizeById_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductDeliverSizeRep.GetProductDeliverSizeByIdAsync(requestBody.ID);
            if (result.Status)
            {
                var resultVM = _mapper.Map<RowResultObject<ProductDeliverSizeVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("ExistProductDeliverSize_Base")]
        public async Task<ActionResult<BitResultObject>> ExistProductDeliverSize_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductDeliverSizeRep.ExistProductDeliverSizeAsync(requestBody.ID);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddProductDeliverSize_Base")]
        public async Task<ActionResult<BitResultObject>> AddProductDeliverSize_Base(AddEditProductDeliverSizeRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            ProductDeliverSize ProductDeliverSize = new ProductDeliverSize()
            {
                CreateDate = DateTime.Now.ToShamsi(),
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                ProductSizeId = requestBody.ProductSizeId,
                ProductDeliverId = requestBody.ProductDeliverId,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),
                
            };
            var result = await _ProductDeliverSizeRep.AddProductDeliverSizeAsync(ProductDeliverSize);
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

        [HttpPut("EditProductDeliverSize_Base")]
        public async Task<ActionResult<BitResultObject>> EditProductDeliverSize_Base(AddEditProductDeliverSizeRequestBody requestBody)
        {
            var result = new BitResultObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var theRow = await _ProductDeliverSizeRep.GetProductDeliverSizeByIdAsync(requestBody.ID);
            if (!theRow.Status)
            {
                result.Status = theRow.Status;
                result.ErrorMessage = theRow.ErrorMessage;
            }

            ProductDeliverSize ProductDeliverSize = new ProductDeliverSize()
            {
                CreateDate = theRow.Result.CreateDate,
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                ProductSizeId = requestBody.ProductSizeId,
                ProductDeliverId = requestBody.ProductDeliverId,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),

            };
            result = await _ProductDeliverSizeRep.EditProductDeliverSizeAsync(ProductDeliverSize);
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

        [HttpDelete("DeleteProductDeliverSize_Base")]
        public async Task<ActionResult<BitResultObject>> DeleteProductDeliverSize_Base(GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductDeliverSizeRep.RemoveProductDeliverSizeAsync(requestBody.ID);
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
