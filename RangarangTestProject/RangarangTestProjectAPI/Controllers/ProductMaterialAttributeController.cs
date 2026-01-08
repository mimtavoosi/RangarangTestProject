using RangarangTestProjectDATA.DataLayer.Repositories;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using RangarangTestProjectDATA.Tools;
using RangarangTestProjectAPI.Models;
using RangarangTestProjectAPI.Models.Public;
using RangarangTestProjectAPI.Models.ProductMaterialAttribute;
using RangarangTestProjectAPI.Validations;
using RangarangTestProjectAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RangarangTestProjectAPI.Controllers
{
    [Route("ProductMaterialAttribute")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]


    public class ProductMaterialAttributeController : ControllerBase
    {
        IProductMaterialAttributeRep _ProductMaterialAttributeRep;
        ILogRep _logRep;
        private readonly IMapper _mapper;


        public ProductMaterialAttributeController(IProductMaterialAttributeRep ProductMaterialAttributeRep,ILogRep logRep,IMapper mapper)
        {
           _ProductMaterialAttributeRep = ProductMaterialAttributeRep;
           _logRep = logRep;
            _mapper = mapper;   
        }

        [HttpGet("GetAllProductMaterialAttributes_Base")]
        public async Task<ActionResult<ListResultObject<ProductMaterialAttributeVM>>> GetAllProductMaterialAttributes_Base([FromQuery] GetProductMaterialAttributeListRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductMaterialAttributeRep.GetAllProductMaterialAttributesAsync(requestBody.ProductMaterialId,requestBody.MaterialAttributeId,requestBody.CreatorId,requestBody.PageIndex,requestBody.PageSize,requestBody.SearchText,requestBody.SortQuery);
            if (result.Status)
            {
                var resultVM = _mapper.Map<ListResultObject<ProductMaterialAttributeVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [HttpGet("GetProductMaterialAttributeById_Base")]
        public async Task<ActionResult<RowResultObject<ProductMaterialAttributeVM>>> GetProductMaterialAttributeById_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductMaterialAttributeRep.GetProductMaterialAttributeByIdAsync(requestBody.ID);
            if (result.Status)
            {
                var resultVM = _mapper.Map<RowResultObject<ProductMaterialAttributeVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("ExistProductMaterialAttribute_Base")]
        public async Task<ActionResult<BitResultObject>> ExistProductMaterialAttribute_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductMaterialAttributeRep.ExistProductMaterialAttributeAsync(requestBody.ID);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddProductMaterialAttribute_Base")]
        public async Task<ActionResult<BitResultObject>> AddProductMaterialAttribute_Base(AddEditProductMaterialAttributeRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            ProductMaterialAttribute ProductMaterialAttribute = new ProductMaterialAttribute()
            {
                CreateDate = DateTime.Now.ToShamsi(),
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                MaterialAttributeId = requestBody.MaterialAttributeId,
                ProductMaterialId = requestBody.ProductMaterialId,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),
                
            };
            var result = await _ProductMaterialAttributeRep.AddProductMaterialAttributeAsync(ProductMaterialAttribute);
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

        [HttpPut("EditProductMaterialAttribute_Base")]
        public async Task<ActionResult<BitResultObject>> EditProductMaterialAttribute_Base(AddEditProductMaterialAttributeRequestBody requestBody)
        {
            var result = new BitResultObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var theRow = await _ProductMaterialAttributeRep.GetProductMaterialAttributeByIdAsync(requestBody.ID);
            if (!theRow.Status)
            {
                result.Status = theRow.Status;
                result.ErrorMessage = theRow.ErrorMessage;
            }

            ProductMaterialAttribute ProductMaterialAttribute = new ProductMaterialAttribute()
            {
                CreateDate = theRow.Result.CreateDate,
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                MaterialAttributeId = requestBody.MaterialAttributeId,
                ProductMaterialId = requestBody.ProductMaterialId,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),

            };
            result = await _ProductMaterialAttributeRep.EditProductMaterialAttributeAsync(ProductMaterialAttribute);
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

        [HttpDelete("DeleteProductMaterialAttribute_Base")]
        public async Task<ActionResult<BitResultObject>> DeleteProductMaterialAttribute_Base(GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductMaterialAttributeRep.RemoveProductMaterialAttributeAsync(requestBody.ID);
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
