using RangarangTestProjectDATA.DataLayer.Repositories;
using RangarangTestProjectDATA.DataLayer.Services;
using RangarangTestProjectDATA.Domain;
using RangarangTestProjectDATA.ResultObjects;
using RangarangTestProjectDATA.Tools;
using RangarangTestProjectAPI.Models.Authenticate;
using RangarangTestProjectAPI.Tools;
using RangarangTestProjectAPI.ViewModels;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RangarangTestProjectAPI.Controllers
{
    [Route("Authentication")]
    [ApiController]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRep _userRep;
        private readonly ILogRep _logRep;
        private readonly ITokenRep _tokenRep;
        private readonly IMapper _mapper;

        public AuthenticationController(IConfiguration configuration, IUserRep userRep,ILogRep logRep,ITokenRep tokenRep,IMapper mapper)
        {
            _configuration = configuration;
            _userRep = userRep;
            _logRep = logRep;
            _tokenRep = tokenRep;
            _mapper = mapper;  
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult<RowResultObject<AuthenticationResultBody>>> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            RowResultObject<AuthenticationResultBody> result = new RowResultObject<AuthenticationResultBody>();
            RowResultObject<User> authenticateResult = new RowResultObject<User>();

            try
            {
                authenticateResult = await _userRep.AuthenticateAsync(authenticationRequestBody.UserName, authenticationRequestBody.Password, 1);


                result.Status = authenticateResult.Status;
                result.ErrorMessage = authenticateResult.ErrorMessage;

                if (authenticateResult.Status)
                {
                   
                    var refreshToken = ToolBox.GenerateToken(); // تولید رفرش توکن
                    var accessToken = ToolBox.GenerateAccessToken(authenticateResult.Result); // تولید رفرش توکن
                    var refreshTokenExpiryDate = DateTime.Now.ToShamsi().AddDays(30); // تنظیم تاریخ انقضای رفرش توکن برای 30 روز


                    var refreshTokenRecord = new Token
                    {
                        CreatorId = authenticateResult.Result.ID,
                        TokenValue = refreshToken, // ذخیره رفرش توکن
                        Type = "RefreshToken", // نوع: RefreshToken
                        Status = true,
                        CreateDate = DateTime.Now.ToShamsi(),
                        UpdateDate = DateTime.Now.ToShamsi(),
                        ExpiryDate = refreshTokenExpiryDate // تاریخ انقضا
                    };

                    var saverefreshToken = await _tokenRep.AddTokenAsync(refreshTokenRecord);
                    if (saverefreshToken.Status)
                    {
                        result.Status = authenticateResult.Status;
                        result.ErrorMessage = authenticateResult.ErrorMessage;
                        result.Result = new AuthenticationResultBody()
                        {
                            RefreshToken = refreshToken, // بازگرداندن رفرش توکن
                            AccessToken = accessToken, // بازگرداندن اکسس توکن
                            User = _mapper.Map<UserVM>(authenticateResult.Result),

                        };

                        #region AddLog
                        Log log = new Log()
                        {
                            CreateDate = DateTime.Now.ToShamsi(),
                            UpdateDate = DateTime.Now.ToShamsi(),
                            LogTime = DateTime.Now.ToShamsi(),
                            ActionName = this.ControllerContext.RouteData.Values["action"].ToString(),
                            CreatorId = authenticateResult.Result.ID,
                        };
                        await _logRep.AddLogAsync(log);
                        #endregion

                        return Ok(result);
                    }

                    else
                    {
                        result.Status = saverefreshToken.Status;
                        result.ErrorMessage = saverefreshToken.ErrorMessage;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ErrorMessage = $"{ex.Message}\n{ex.InnerException?.Message}";
            }


            return BadRequest(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<RowResultObject<RefreshTokenResultBody>>> RefreshToken(RefreshTokenRequestBody requestBody)
        {
            RowResultObject<RefreshTokenResultBody> result = new RowResultObject<RefreshTokenResultBody>();

            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }

            var refreshTokenRecord = await _tokenRep.FindTokenAsync(requestBody.RefreshToken, "RefreshToken");

            if (!refreshTokenRecord.Status && refreshTokenRecord.Result == null)
            {
                result.ErrorMessage = "رفرش توکن نامعتبر است";
                result.Status = false;
                return BadRequest(result);
            }

            var expireTokenResult = await _tokenRep.MakeTokenExpireAsync(refreshTokenRecord.Result.ID);

            if (expireTokenResult.Status)
            {
                var user = await _userRep.GetUserByIdAsync(refreshTokenRecord.Result.CreatorId);
                var refreshToken = ToolBox.GenerateToken(); // تولید رفرش توکن
                var accessToken = ToolBox.GenerateAccessToken(user.Result); // تولید رفرش توکن
                var refreshTokenExpiryDate = DateTime.Now.ToShamsi().AddDays(30); // تنظیم تاریخ انقضای رفرش توکن برای 30 روز


                var newrefreshTokenRecord = new Token
                {
                    CreatorId = user.Result.ID,
                    TokenValue = refreshToken, // ذخیره رفرش توکن
                    Type = "RefreshToken", // نوع: RefreshToken
                    Status = true,
                    CreateDate = DateTime.Now.ToShamsi(),
                    UpdateDate = DateTime.Now.ToShamsi(),
                    ExpiryDate = refreshTokenExpiryDate // تاریخ انقضا
                };

                var saverefreshToken = await _tokenRep.AddTokenAsync(newrefreshTokenRecord);

                if (saverefreshToken.Status)
                {
                    result.Status = user.Status;
                    result.ErrorMessage = user.ErrorMessage;
                    result.Result = new RefreshTokenResultBody()
                    {
                        RefreshToken = refreshToken, // بازگرداندن رفرش توکن
                        AccessToken = accessToken, // بازگرداندن اکسس توکن
                    };


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
            }
            else
            {
                result.Status = expireTokenResult.Status;
                result.ErrorMessage = expireTokenResult.ErrorMessage;
            }
            return BadRequest(result);
        }

        [HttpPost("Signup")]
        public async Task<ActionResult<BitResultObject>> Signup(SignupRequestBody signupRequestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(signupRequestBody);
            }

            BitResultObject result = new BitResultObject();


            var validUserName = await _userRep.ExistUserAsync(signupRequestBody.UserName,"username");

            if (validUserName.Status)
            {
                result.Status = !validUserName.Status;
                result.ErrorMessage = "نام کاربری (شماره موبایل) تکراری است";
                return BadRequest(result);
            }

         
            var validEmail = await _userRep.ExistUserAsync(signupRequestBody.Email, "email");

            if (validEmail.Status)
            {
                result.Status = !validEmail.Status;
                result.ErrorMessage = "پست الکترونیک تکراری است";
                return BadRequest(result);
            }

            var validNationalCode = await _userRep.ExistUserAsync(signupRequestBody.NationalCode, "nationalcode");

            if (validNationalCode.Status)
            {
                result.Status = !validNationalCode.Status;
                result.ErrorMessage = "کد ملی تکراری است";
                return BadRequest(result);
            }

            if (result.Status)
            {
                User user = new User()
                {
                    FirstName = signupRequestBody.FirstName,
                    LastName = signupRequestBody.LastName,
                    Username = signupRequestBody.UserName,
                    Email = signupRequestBody.Email,
                    NationalCode = signupRequestBody.NationalCode,
                    PasswordHash = signupRequestBody.Password.ToHash(),
                    CreateDate = DateTime.Now.ToShamsi(),
                    UpdateDate = DateTime.Now.ToShamsi(),
                    CreatorId = int.Parse(User?.FindFirst("userId")?.Value?.ToString() ?? "0"),
                };
            
                result = await _userRep.AddUserAsync(user);

                if (result.Status)
                {
                    #region AddLog

                    Log log = new Log()
                    {
                        CreateDate = DateTime.Now.ToShamsi(),
                        UpdateDate = DateTime.Now.ToShamsi(),
                        LogTime = DateTime.Now.ToShamsi(),
                        ActionName = this.ControllerContext.RouteData.Values["action"].ToString(),
                        CreatorId = result.ID,
                    };
                    await _logRep.AddLogAsync(log);

                    #endregion

                    return Ok(result);
                }
            }
            return BadRequest(result);
        }


        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<RowResultObject<string>>> ForgotPassword(ForgotPasswordRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            RowResultObject<string> result = new RowResultObject<string>();

            if (string.IsNullOrEmpty(requestBody.Email))
            {
                result.Status = false;
                result.ErrorMessage = $"ورود حداقل یکی از مقادیر خواسته شده الزامی است";
                return BadRequest(result);
            }

            var resetTokenExpiryDate = DateTime.Now.ToShamsi().AddHours(2);

            var existLogin = await _userRep.ExistUserAsync(requestBody.Email, "email");

            if (existLogin.Status)
            {
                var user = await _userRep.GetUserByIdAsync(existLogin.ID);
                var resetToken = ToolBox.GenerateToken(user.Result.ID); // تولید رفرش توکن

                if (user.Status)
                {

                    var newresetTokenRecord = new Token
                    {
                        CreatorId = user.Result.ID,
                        TokenValue = resetToken, // ذخیره رفرش توکن
                        Type = "ResetPassword", // نوع: ResetPassword
                        Status = true,
                        CreateDate = DateTime.Now.ToShamsi(),
                        UpdateDate = DateTime.Now.ToShamsi(),
                        ExpiryDate = resetTokenExpiryDate // تاریخ انقضا
                    };

                    var saverefreshToken = await _tokenRep.AddTokenAsync(newresetTokenRecord);

                    if (saverefreshToken.Status)
                    {

                        var fullName = $"{user.Result.FirstName} {user.Result.LastName}";
                        var messageText = ToolBox.MakeResetPasswordMessage(fullName, resetToken);
                        bool sentState = ToolBox.SendEmail(requestBody.Email, "بازنشانی کلمه عبور", messageText);

                        #region AddLog
                        Log log = new Log()
                        {
                            CreateDate = DateTime.Now.ToShamsi(),
                            UpdateDate = DateTime.Now.ToShamsi(),
                            LogTime = DateTime.Now.ToShamsi(),
                            ActionName = this.ControllerContext.RouteData.Values["action"].ToString(),
                            CreatorId = user.Result.ID,
                        };
                        await _logRep.AddLogAsync(log);
                        #endregion


                        if (sentState)
                        {
                            result.Status = sentState;
                            result.ErrorMessage = $"ایمیلی حاوی لینک بازنشانی رمز عبور برای شما ارسال شد";
                            result.Result = resetToken;
                        }
                        else
                        {
                            result.Status = sentState;
                            result.ErrorMessage = $"در ارسال ایمیلی مشکلی بوجود آمد لطفا دوباره تلاش کنید";
                            result.Result = resetToken;
                        }

                        return Ok(result);
                    }
                    else
                    {
                        result.Status = saverefreshToken.Status;
                        result.ErrorMessage = saverefreshToken.ErrorMessage;
                    }
                }
                else
                {
                    result.Status = user.Status;
                    result.ErrorMessage = user.ErrorMessage;
                }
            }
            else
            {
                result.Status = false;
                result.ErrorMessage = $"پست الکترونیک {requestBody.Email} در سیستم وجود ندارد";
            }

            return BadRequest(result);
        }


        [HttpPost("ResetPassword")]
        public async Task<ActionResult<BitResultObject>> ResetPassword(ResetPasswordRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            BitResultObject result = new BitResultObject();

            if (string.IsNullOrEmpty(requestBody.Token))
            {
                result.Status = false;
                result.ErrorMessage = $"ورود حداقل یکی از مقادیر توکن الزامی است";
            }

            if (!string.IsNullOrEmpty(requestBody.Token))
            {
                int userId = int.Parse(requestBody.Token.Split('-')[0]);

                var existLogin = await _userRep.ExistUserAsync(userId.ToString(), "id");

                if (userId > 0 && existLogin.Status)
                {
                    var user = await _userRep.GetUserByIdAsync(userId);

                    if (user.Status)
                    {
                        user.Result.PasswordHash = requestBody.NewPassword.ToHash();

                        var updateduser = await _userRep.EditUserAsync(user.Result);

                        if (updateduser.Status)
                        {
                            result.Status = updateduser.Status;
                            result.ErrorMessage = $"تغییر کلمه عبور با موفقیت انجام شد";

                            #region AddLog
                            Log log = new Log()
                            {
                                CreateDate = DateTime.Now.ToShamsi(),
                                UpdateDate = DateTime.Now.ToShamsi(),
                                LogTime = DateTime.Now.ToShamsi(),
                                ActionName = this.ControllerContext.RouteData.Values["action"].ToString(),
                                CreatorId = user.Result.ID,
                            };
                            await _logRep.AddLogAsync(log);
                            #endregion

                            return Ok(result);
                        }
                        else
                        {
                            result.Status = updateduser.Status;
                            result.ErrorMessage = updateduser.ErrorMessage;
                        }
                    }
                    else
                    {
                        result.Status = user.Status;
                        result.ErrorMessage = user.ErrorMessage;
                    }
                }

                else
                {
                    result.Status = false;
                    result.ErrorMessage = "کاربر معتبر نیست";
                }
            }

            return BadRequest(result);
        }

        [HttpGet("CheckToken")]
        public async Task<ActionResult<BitResultObject>> CheckToken([FromQuery] CheckTokenRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            BitResultObject result = new BitResultObject();

            var findToken = await _tokenRep.FindTokenAsync(requestBody.Token,requestBody.TokenType,requestBody.TokenStatus);

            result.Status = findToken.Status;
            result.ErrorMessage = findToken.ErrorMessage;

            if (findToken.Status && findToken.Result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("LogOut")]
        public async Task<ActionResult<BitResultObject>> LogOut(RefreshTokenRequestBody requestBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestBody);
            }
            BitResultObject result = new BitResultObject() { Status = true,ErrorMessage =""};

             var refreshTokenRecord = await _tokenRep.FindTokenAsync(requestBody.RefreshToken, "RefreshToken");

            if (refreshTokenRecord.Status && refreshTokenRecord.Result != null)
            {
                var expireTokenResult = await _tokenRep.MakeTokenExpireAsync(refreshTokenRecord.Result.ID);

                if (expireTokenResult.Status)
                {
                    result.Status = expireTokenResult.Status;
                    result.ErrorMessage = $"کاربر از سیستم خارج شد";

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
                }
                else
                {
                    result.Status = expireTokenResult.Status;
                    result.ErrorMessage = expireTokenResult.ErrorMessage;
                    return BadRequest(result);
                }
            }

                return Ok(result);
        }


     

    }
}