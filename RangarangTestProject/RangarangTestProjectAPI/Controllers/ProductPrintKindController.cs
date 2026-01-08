using RangarangTestProjectDATA.DataLayer.Repositories;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using RangarangTestProjectDATA.Tools;
using RangarangTestProjectAPI.Models;
using RangarangTestProjectAPI.Models.Public;
using RangarangTestProjectAPI.Models.ProductPrintKind;
using RangarangTestProjectAPI.Validations;
using RangarangTestProjectAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RangarangTestProjectAPI.Controllers
{
    [Route("ProductPrintKind")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]


    public class ProductPrintKindController : ControllerBase
    {
        IProductPrintKindRep _ProductPrintKindRep;
        ILogRep _logRep;
        private readonly IMapper _mapper;


        public ProductPrintKindController(IProductPrintKindRep ProductPrintKindRep,ILogRep logRep,IMapper mapper)
        {
           _ProductPrintKindRep = ProductPrintKindRep;
           _logRep = logRep;
            _mapper = mapper;   
        }

        [HttpGet("GetAllProductPrintKinds_Base")]
        public async Task<ActionResult<ListResultObject<ProductPrintKindVM>>> GetAllProductPrintKinds_Base([FromQuery] GetProductPrintKindListRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductPrintKindRep.GetAllProductPrintKindsAsync(requestBody.ProductId,requestBody.PrintKindId,requestBody.CreatorId,requestBody.PageIndex,requestBody.PageSize,requestBody.SearchText,requestBody.SortQuery);
            if (result.Status)
            {
                var resultVM = _mapper.Map<ListResultObject<ProductPrintKindVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [HttpGet("GetProductPrintKindById_Base")]
        public async Task<ActionResult<RowResultObject<ProductPrintKindVM>>> GetProductPrintKindById_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductPrintKindRep.GetProductPrintKindByIdAsync(requestBody.ID);
            if (result.Status)
            {
                var resultVM = _mapper.Map<RowResultObject<ProductPrintKindVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("ExistProductPrintKind_Base")]
        public async Task<ActionResult<BitResultObject>> ExistProductPrintKind_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductPrintKindRep.ExistProductPrintKindAsync(requestBody.ID);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddProductPrintKind_Base")]
        public async Task<ActionResult<BitResultObject>> AddProductPrintKind_Base(AddEditProductPrintKindRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            ProductPrintKind ProductPrintKind = new ProductPrintKind()
            {
                CreateDate = DateTime.Now.ToShamsi(),
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                PrintKindId = requestBody.PrintKindId,
                ProductId = requestBody.ProductId,
                IsJeld = requestBody.IsJeld,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value.ToString()),
                
            };
            var result = await _ProductPrintKindRep.AddProductPrintKindAsync(ProductPrintKind);
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

        [HttpPut("EditProductPrintKind_Base")]
        public async Task<ActionResult<BitResultObject>> EditProductPrintKind_Base(AddEditProductPrintKindRequestBody requestBody)
        {
            var result = new BitResultObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var theRow = await _ProductPrintKindRep.GetProductPrintKindByIdAsync(requestBody.ID);
            if (!theRow.Status)
            {
                result.Status = theRow.Status;
                result.ErrorMessage = theRow.ErrorMessage;
            }

            ProductPrintKind ProductPrintKind = new ProductPrintKind()
            {
                CreateDate = theRow.Result.CreateDate,
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                PrintKindId = requestBody.PrintKindId,
                ProductId = requestBody.ProductId,
                IsJeld = requestBody.IsJeld,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value.ToString()),

            };
            result = await _ProductPrintKindRep.EditProductPrintKindAsync(ProductPrintKind);
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

        [HttpDelete("DeleteProductPrintKind_Base")]
        public async Task<ActionResult<BitResultObject>> DeleteProductPrintKind_Base(GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductPrintKindRep.RemoveProductPrintKindAsync(requestBody.ID);
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
