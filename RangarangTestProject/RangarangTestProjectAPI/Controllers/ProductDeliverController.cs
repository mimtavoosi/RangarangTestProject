using RangarangTestProjectDATA.DataLayer.Repositories;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using RangarangTestProjectDATA.Tools;
using RangarangTestProjectAPI.Models;
using RangarangTestProjectAPI.Models.Public;
using RangarangTestProjectAPI.Models.ProductDeliver;
using RangarangTestProjectAPI.Validations;
using RangarangTestProjectAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RangarangTestProjectAPI.Controllers
{
    [Route("ProductDeliver")]
    [ApiController]
    //[Authorize]
    [Produces("application/json")]


    public class ProductDeliverController : ControllerBase
    {
        IProductDeliverRep _ProductDeliverRep;
        ILogRep _logRep;
        private readonly IMapper _mapper;


        public ProductDeliverController(IProductDeliverRep ProductDeliverRep,ILogRep logRep,IMapper mapper)
        {
           _ProductDeliverRep = ProductDeliverRep;
           _logRep = logRep;
            _mapper = mapper;   
        }

        [HttpGet("GetAllProductDelivers_Base")]
        public async Task<ActionResult<ListResultObject<ProductDeliverVM>>> GetAllProductDelivers_Base([FromQuery] GetProductDeliverListRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductDeliverRep.GetAllProductDeliversAsync(requestBody.ProductId,requestBody.CreatorId,requestBody.PageIndex,requestBody.PageSize,requestBody.SearchText,requestBody.SortQuery);
            if (result.Status)
            {
                var resultVM = _mapper.Map<ListResultObject<ProductDeliverVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [HttpGet("GetProductDeliverById_Base")]
        public async Task<ActionResult<RowResultObject<ProductDeliverVM>>> GetProductDeliverById_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductDeliverRep.GetProductDeliverByIdAsync(requestBody.ID);
            if (result.Status)
            {
                var resultVM = _mapper.Map<RowResultObject<ProductDeliverVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("ExistProductDeliver_Base")]
        public async Task<ActionResult<BitResultObject>> ExistProductDeliver_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductDeliverRep.ExistProductDeliverAsync(requestBody.ID);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddProductDeliver_Base")]
        public async Task<ActionResult<BitResultObject>> AddProductDeliver_Base(AddEditProductDeliverRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            ProductDeliver ProductDeliver = new ProductDeliver()
            {
                CreateDate = DateTime.Now.ToShamsi(),
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                CalcType = requestBody.CalcType,
                Price = requestBody.Price,
                Name = requestBody.Name,
                PrintSide = requestBody.PrintSide,
                IsIncreased = requestBody.IsIncreased,
                StartCirculation = requestBody.StartCirculation,
                ProductId = requestBody.EndCirculation,
                EndCirculation = requestBody.ProductId,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),
                
            };
            var result = await _ProductDeliverRep.AddProductDeliverAsync(ProductDeliver);
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

        [HttpPut("EditProductDeliver_Base")]
        public async Task<ActionResult<BitResultObject>> EditProductDeliver_Base(AddEditProductDeliverRequestBody requestBody)
        {
            var result = new BitResultObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var theRow = await _ProductDeliverRep.GetProductDeliverByIdAsync(requestBody.ID);
            if (!theRow.Status)
            {
                result.Status = theRow.Status;
                result.ErrorMessage = theRow.ErrorMessage;
            }

            ProductDeliver ProductDeliver = new ProductDeliver()
            {
                CreateDate = theRow.Result.CreateDate,
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                CalcType = requestBody.CalcType,
                Price = requestBody.Price,
                Name = requestBody.Name,
                PrintSide = requestBody.PrintSide,
                IsIncreased = requestBody.IsIncreased,
                StartCirculation = requestBody.StartCirculation,
                ProductId = requestBody.EndCirculation,
                EndCirculation = requestBody.ProductId,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),

            };
            result = await _ProductDeliverRep.EditProductDeliverAsync(ProductDeliver);
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

        [HttpDelete("DeleteProductDeliver_Base")]
        public async Task<ActionResult<BitResultObject>> DeleteProductDeliver_Base(GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductDeliverRep.RemoveProductDeliverAsync(requestBody.ID);
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
