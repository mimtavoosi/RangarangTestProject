using RangarangTestProjectDATA.DataLayer.Repositories;
using RangarangTestProjectDATA.DataLayer.Services;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using RangarangTestProjectDATA.Tools;
using RangarangTestProjectAPI.Models;
using RangarangTestProjectAPI.Models.Log;
using RangarangTestProjectAPI.Models.Public;
using RangarangTestProjectAPI.Validations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static RangarangTestProjectAPI.Tools.ToolBox;

namespace RangarangTestProjectAPI.Controllers
{
    [Route("Log")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]


    public class LogController : ControllerBase
    {
        ILogRep _LogRep;

        public LogController(ILogRep LogRep)
        {
           _LogRep = LogRep;
        }

        [HttpGet("GetAllLogs_Base")]
        public async Task<ActionResult<ListResultObject<Log>>> GetAllLogs_Base([FromQuery] GetLogListRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _LogRep.GetAllLogsAsync(requestBody.CreatorId,requestBody.PageIndex,requestBody.PageSize,requestBody.SearchText,requestBody.SortQuery);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetLogById_Base")]
        public async Task<ActionResult<RowResultObject<Log>>> GetLogById_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _LogRep.GetLogByIdAsync(requestBody.ID);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("ExistLog_Base")]
        public async Task<ActionResult<BitResultObject>> ExistLog_Base([FromQuery] GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _LogRep.ExistLogAsync(requestBody.ID);
            if (string.IsNullOrEmpty(result.ErrorMessage))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddLog_Base")]
        public async Task<ActionResult<BitResultObject>> AddLog_Base(AddEditLogRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            Log Log = new Log()
            {
                CreateDate = DateTime.Now.ToShamsi(),
                UpdateDate = DateTime.Now.ToShamsi(),
                ActionName = requestBody.ActionName,
                LogTime= requestBody.LogTime,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),
            };
            var result = await _LogRep.AddLogAsync(Log);
            if (result.Status)
            {
               return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("EditLog_Base")]
        public async Task<ActionResult<BitResultObject>> EditLog_Base(AddEditLogRequestBody requestBody)
        {
            var result = new BitResultObject();
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var theRow = await _LogRep.GetLogByIdAsync(requestBody.ID);
            if (!theRow.Status)
            {
                result.Status = theRow.Status;
                result.ErrorMessage = theRow.ErrorMessage;
            }

            Log Log = new Log()
            {
                CreateDate = theRow.Result.CreateDate,
                ID = requestBody.ID,
                UpdateDate = DateTime.Now.ToShamsi(),
                ActionName = requestBody.ActionName,
                LogTime = requestBody.LogTime,
                CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),

            };
            result = await _LogRep.EditLogAsync(Log);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteLog_Base")]
        public async Task<ActionResult<BitResultObject>> DeleteLog_Base(GetRowRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            var result = await _LogRep.RemoveLogAsync(requestBody.ID);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
