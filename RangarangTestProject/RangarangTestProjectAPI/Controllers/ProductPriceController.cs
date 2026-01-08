using RangarangTestProjectDATA.DataLayer.Repositories;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using RangarangTestProjectDATA.Tools;
using RangarangTestProjectAPI.Models;
using RangarangTestProjectAPI.Models.Public;
using RangarangTestProjectAPI.Models.ProductPrice;
using RangarangTestProjectAPI.Validations;
using RangarangTestProjectAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RangarangTestProjectAPI.Controllers
{
    [Route("ProductPrice")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]


    public class ProductPriceController : ControllerBase
    {
        IProductPriceRep _ProductPriceRep;
        ILogRep _logRep;
        private readonly IMapper _mapper;


        public ProductPriceController(IProductPriceRep ProductPriceRep,ILogRep logRep,IMapper mapper)
        {
           _ProductPriceRep = ProductPriceRep;
           _logRep = logRep;
            _mapper = mapper;   
        }

        [HttpGet("GetAllProductPrices_Base")]
        public async Task<ActionResult<ListResultObject<ProductPriceVM>>> GetAllProductPrices_Base([FromQuery] GetProductPriceListRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductPriceRep.GetAllProductPricesAsync(requestBody.ProductSizeId,requestBody.ProductMaterialId,requestBody.ProductMaterialAttributeId,requestBody.ProductPrintKindId,requestBody.CreatorId,requestBody.PageIndex,requestBody.PageSize,requestBody.SearchText,requestBody.SortQuery);
            if (result.Status)
            {
                var resultVM = _mapper.Map<ListResultObject<ProductPriceVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [HttpGet("GetProductPriceById_Base")]
        public async Task<ActionResult<RowResultObject<ProductPriceVM>>> GetProductPriceById_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductPriceRep.GetProductPriceByIdAsync(requestBody.ID);
            if (result.Status)
            {
                var resultVM = _mapper.Map<RowResultObject<ProductPriceVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("ExistProductPrice_Base")]
        public async Task<ActionResult<BitResultObject>> ExistProductPrice_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductPriceRep.ExistProductPriceAsync(requestBody.ID);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddProductPrice_Base")]
        public async Task<ActionResult<BitResultObject>> AddProductPrice_Base(AddEditProductPriceRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            ProductPrice ProductPrice = new ProductPrice()
            {
                CreateDate = DateTime.Now.ToShamsi(),
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                Circulation = requestBody.Circulation,
                CopyCount = requestBody.CopyCount,
                PageCount = requestBody.PageCount,
                IsDoubleSided = requestBody.IsDoubleSided,
                ProductPrintKindId = requestBody.ProductPrintKindId,
                Price = requestBody.Price,
                ProductMaterialAttributeId = requestBody.ProductMaterialAttributeId,
                ProductMaterialId = requestBody.ProductMaterialId,
                IsJeld = requestBody.IsJeld,
                ProductSizeId = requestBody.ProductSizeId,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value.ToString()),
                
            };
            var result = await _ProductPriceRep.AddProductPriceAsync(ProductPrice);
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

        [HttpPut("EditProductPrice_Base")]
        public async Task<ActionResult<BitResultObject>> EditProductPrice_Base(AddEditProductPriceRequestBody requestBody)
        {
            var result = new BitResultObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var theRow = await _ProductPriceRep.GetProductPriceByIdAsync(requestBody.ID);
            if (!theRow.Status)
            {
                result.Status = theRow.Status;
                result.ErrorMessage = theRow.ErrorMessage;
            }

            ProductPrice ProductPrice = new ProductPrice()
            {
                CreateDate = theRow.Result.CreateDate,
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                Circulation = requestBody.Circulation,
                CopyCount = requestBody.CopyCount,
                PageCount = requestBody.PageCount,
                IsDoubleSided = requestBody.IsDoubleSided,
                ProductPrintKindId = requestBody.ProductPrintKindId,
                Price = requestBody.Price,
                ProductMaterialAttributeId = requestBody.ProductMaterialAttributeId,
                ProductMaterialId = requestBody.ProductMaterialId,
                IsJeld = requestBody.IsJeld,
                ProductSizeId = requestBody.ProductSizeId,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value.ToString()),

            };
            result = await _ProductPriceRep.EditProductPriceAsync(ProductPrice);
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

        [HttpDelete("DeleteProductPrice_Base")]
        public async Task<ActionResult<BitResultObject>> DeleteProductPrice_Base(GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductPriceRep.RemoveProductPriceAsync(requestBody.ID);
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
