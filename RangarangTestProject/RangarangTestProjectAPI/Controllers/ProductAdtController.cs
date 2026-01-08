using RangarangTestProjectDATA.DataLayer.Repositories;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using RangarangTestProjectDATA.Tools;
using RangarangTestProjectAPI.Models;
using RangarangTestProjectAPI.Models.Public;
using RangarangTestProjectAPI.Models.ProductAdt;
using RangarangTestProjectAPI.Validations;
using RangarangTestProjectAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RangarangTestProjectAPI.Controllers
{
    [Route("ProductAdt")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]


    public class ProductAdtController : ControllerBase
    {
        IProductAdtRep _ProductAdtRep;
        ILogRep _logRep;
        private readonly IMapper _mapper;


        public ProductAdtController(IProductAdtRep ProductAdtRep,ILogRep logRep,IMapper mapper)
        {
           _ProductAdtRep = ProductAdtRep;
           _logRep = logRep;
            _mapper = mapper;   
        }

        [HttpGet("GetAllProductAdts_Base")]
        public async Task<ActionResult<ListResultObject<ProductAdtVM>>> GetAllProductAdts_Base([FromQuery] GetProductAdtListRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductAdtRep.GetAllProductAdtsAsync(requestBody.ProductId,requestBody.AdtId,requestBody.CreatorId,requestBody.PageIndex,requestBody.PageSize,requestBody.SearchText,requestBody.SortQuery);
            if (result.Status)
            {
                var resultVM = _mapper.Map<ListResultObject<ProductAdtVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [HttpGet("GetProductAdtById_Base")]
        public async Task<ActionResult<RowResultObject<ProductAdtVM>>> GetProductAdtById_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductAdtRep.GetProductAdtByIdAsync(requestBody.ID);
            if (result.Status)
            {
                var resultVM = _mapper.Map<RowResultObject<ProductAdtVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("ExistProductAdt_Base")]
        public async Task<ActionResult<BitResultObject>> ExistProductAdt_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductAdtRep.ExistProductAdtAsync(requestBody.ID);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddProductAdt_Base")]
        public async Task<ActionResult<BitResultObject>> AddProductAdt_Base(AddEditProductAdtRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            ProductAdt ProductAdt = new ProductAdt()
            {
                CreateDate = DateTime.Now.ToShamsi(),
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                AdtId = requestBody.AdtId,
                ProductId = requestBody.ProductId,  
                Count = requestBody.Count,
                IsJeld = requestBody.IsJeld,
                Required = requestBody.Required,
                Side = requestBody.Side,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value.ToString()),
                
            };
            var result = await _ProductAdtRep.AddProductAdtAsync(ProductAdt);
            if (result.Status)
            {
                #region AddLog

                Log log = new Log()
                {
                    CreateDate = DateTime.Now.ToShamsi(),
                    UpdateDate = DateTime.Now.ToShamsi(),
                    LogTime = DateTime.Now.ToShamsi(),
                    ActionName = this.ControllerContext.RouteData.Values["action"].ToString(),
                    CreatorId = int.Parse(User?.FindFirst("userId")?.Value.ToString()),
                    
                };
                await _logRep.AddLogAsync(log);

                #endregion


                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("EditProductAdt_Base")]
        public async Task<ActionResult<BitResultObject>> EditProductAdt_Base(AddEditProductAdtRequestBody requestBody)
        {
            var result = new BitResultObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var theRow = await _ProductAdtRep.GetProductAdtByIdAsync(requestBody.ID);
            if (!theRow.Status)
            {
                result.Status = theRow.Status;
                result.ErrorMessage = theRow.ErrorMessage;
            }

            ProductAdt ProductAdt = new ProductAdt()
            {
                CreateDate = theRow.Result.CreateDate,
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                AdtId = requestBody.AdtId,
                ProductId = requestBody.ProductId,
                Count = requestBody.Count,
                IsJeld = requestBody.IsJeld,
                Required = requestBody.Required,
                Side = requestBody.Side,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value.ToString()),

            };
            result = await _ProductAdtRep.EditProductAdtAsync(ProductAdt);
            if (result.Status)
            {

                #region AddLog

                Log log = new Log()
                {
                    CreateDate = DateTime.Now.ToShamsi(),
                    UpdateDate = DateTime.Now.ToShamsi(),
                    LogTime = DateTime.Now.ToShamsi(),
                    ActionName = this.ControllerContext.RouteData.Values["action"].ToString(),
                    CreatorId = int.Parse(User?.FindFirst("userId")?.Value.ToString()),

                };
                await _logRep.AddLogAsync(log);

                #endregion

                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteProductAdt_Base")]
        public async Task<ActionResult<BitResultObject>> DeleteProductAdt_Base(GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductAdtRep.RemoveProductAdtAsync(requestBody.ID);
            if (result.Status)
            {

                #region AddLog

                Log log = new Log()
                {
                    CreateDate = DateTime.Now.ToShamsi(),
                    UpdateDate = DateTime.Now.ToShamsi(),
                    LogTime = DateTime.Now.ToShamsi(),
                    ActionName = this.ControllerContext.RouteData.Values["action"].ToString(),
                    CreatorId = int.Parse(User?.FindFirst("userId")?.Value.ToString()),

                };
                await _logRep.AddLogAsync(log);

                #endregion

                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
