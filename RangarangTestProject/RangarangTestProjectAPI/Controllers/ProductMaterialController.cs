using RangarangTestProjectDATA.DataLayer.Repositories;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using RangarangTestProjectDATA.Tools;
using RangarangTestProjectAPI.Models;
using RangarangTestProjectAPI.Models.Public;
using RangarangTestProjectAPI.Models.ProductMaterial;
using RangarangTestProjectAPI.Validations;
using RangarangTestProjectAPI.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RangarangTestProjectAPI.Controllers
{
    [Route("ProductMaterial")]
    [ApiController]
    //[Authorize]
    [Produces("application/json")]


    public class ProductMaterialController : ControllerBase
    {
        IProductMaterialRep _ProductMaterialRep;
        ILogRep _logRep;
        private readonly IMapper _mapper;


        public ProductMaterialController(IProductMaterialRep ProductMaterialRep,ILogRep logRep,IMapper mapper)
        {
           _ProductMaterialRep = ProductMaterialRep;
           _logRep = logRep;
            _mapper = mapper;   
        }

        [HttpGet("GetAllProductMaterials_Base")]
        public async Task<ActionResult<ListResultObject<ProductMaterialVM>>> GetAllProductMaterials_Base([FromQuery] GetProductMaterialListRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductMaterialRep.GetAllProductMaterialsAsync(requestBody.ProductId,requestBody.MaterialId,requestBody.CreatorId,requestBody.PageIndex,requestBody.PageSize,requestBody.SearchText,requestBody.SortQuery);
            if (result.Status)
            {
                var resultVM = _mapper.Map<ListResultObject<ProductMaterialVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [HttpGet("GetProductMaterialById_Base")]
        public async Task<ActionResult<RowResultObject<ProductMaterialVM>>> GetProductMaterialById_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductMaterialRep.GetProductMaterialByIdAsync(requestBody.ID);
            if (result.Status)
            {
                var resultVM = _mapper.Map<RowResultObject<ProductMaterialVM>>(result);
                return Ok(resultVM);
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("ExistProductMaterial_Base")]
        public async Task<ActionResult<BitResultObject>> ExistProductMaterial_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductMaterialRep.ExistProductMaterialAsync(requestBody.ID);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddProductMaterial_Base")]
        public async Task<ActionResult<BitResultObject>> AddProductMaterial_Base(AddEditProductMaterialRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            ProductMaterial ProductMaterial = new ProductMaterial()
            {
                CreateDate = DateTime.Now.ToShamsi(),
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                IsCombinedMaterial = requestBody.IsCombinedMaterial,
                IsCustomCirculation = requestBody.IsCustomCirculation,
                Weight = requestBody.Weight,
                Required = requestBody.Required,
                ProductId = requestBody.ProductId,
                MaterialId = requestBody.MaterialId,
                IsJeld = requestBody.IsJeld,
                Name = requestBody.Name,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),
                
            };
            var result = await _ProductMaterialRep.AddProductMaterialAsync(ProductMaterial);
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

        [HttpPut("EditProductMaterial_Base")]
        public async Task<ActionResult<BitResultObject>> EditProductMaterial_Base(AddEditProductMaterialRequestBody requestBody)
        {
            var result = new BitResultObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var theRow = await _ProductMaterialRep.GetProductMaterialByIdAsync(requestBody.ID);
            if (!theRow.Status)
            {
                result.Status = theRow.Status;
                result.ErrorMessage = theRow.ErrorMessage;
            }

            ProductMaterial ProductMaterial = new ProductMaterial()
            {
                CreateDate = theRow.Result.CreateDate,
                UpdateDate = DateTime.Now.ToShamsi(),
                ID = requestBody.ID,
                IsCombinedMaterial = requestBody.IsCombinedMaterial,
                IsCustomCirculation = requestBody.IsCustomCirculation,
                Weight = requestBody.Weight,
                Required = requestBody.Required,
                ProductId = requestBody.ProductId,
                MaterialId = requestBody.MaterialId,
                IsJeld = requestBody.IsJeld,
                Name = requestBody.Name,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),

            };
            result = await _ProductMaterialRep.EditProductMaterialAsync(ProductMaterial);
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

        [HttpDelete("DeleteProductMaterial_Base")]
        public async Task<ActionResult<BitResultObject>> DeleteProductMaterial_Base(GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _ProductMaterialRep.RemoveProductMaterialAsync(requestBody.ID);
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
