using LTHL.VIEW_MODELS.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace DevTest.Helpers.AUTH
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AppAuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        private bool isLoginRequired = true;
        public AppAuthorizeAttribute(bool isLoginRequired)
        {
            this.isLoginRequired = isLoginRequired;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //if (!isLoginRequired)
            //{
            //    context.RouteData.Values.Add("userId", Guid.Empty.ToString());
            //    context.RouteData.Values.Add("DeviceToken", "");
            //    return;
            //}

            AuthToken tokenData = null;
            string token = string.Empty;
            token = (context.HttpContext.Request.Headers.Any(x => x.Key == "Authorization")) ? context.HttpContext.Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value.SingleOrDefault().Replace("Bearer ", "") : "";
            if (token == string.Empty && isLoginRequired)
            {
                context.HttpContext.Response.StatusCode = 401;
                context.Result = new JsonResult(new Response<bool> { IsError = true, Message = "Please login to use this functionality.", Data = false });
                return;
            }

            if (!string.IsNullOrEmpty(token))
            {
                var configuration = (IConfiguration)context.HttpContext.RequestServices.GetService(typeof(IConfiguration));
                var keyByteArray = Encoding.ASCII.GetBytes(configuration.GetValue<String>("MySettings:SecretKey"));
                var signinKey = new SymmetricSecurityKey(keyByteArray);

                try
                {
                    SecurityToken validatedToken;
                    var handeler = new JwtSecurityTokenHandler();
                    var we = handeler.ValidateToken(token, new TokenValidationParameters
                    {
                        IssuerSigningKey = signinKey,
                        ValidAudience = "Audience",
                        ValidIssuer = "Issuer",
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(0)
                    }, out validatedToken);

                    var temp = handeler.ReadJwtToken(token);
                    //tokenData = JsonConvert.DeserializeObject<AuthToken>(temp.Claims.FirstOrDefault(x => x.Type.Equals("token"))?.Value);

                    tokenData = new AuthToken
                    {
                        UserId = Convert.ToInt32(temp.Claims.FirstOrDefault(x => x.Type.Equals("nameid"))?.Value),
                        DeviceToken = temp.Claims.FirstOrDefault(x => x.Type.Equals("token"))?.Value
                    };

                    context.RouteData.Values.Add("userId", tokenData.UserId);
                    context.RouteData.Values.Add("DeviceToken", tokenData.DeviceToken);
                }
                catch (Exception ex)
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new JsonResult(new Response<bool> { IsError = true, Message = "Access Denied!", Data = false });
                    return;
                }

                var secretKey = configuration.GetValue<String>("MySettings:SecretKey");

                //var userManagementService = (IUserManagementService)context.HttpContext.RequestServices.GetService(typeof(IUserManagementService));
                //var isAuthenticated = (tokenData.DeviceToken == secretKey) ? true : false; //accountService.IsTokenValid(tokenData.UserId, tokenData.DeviceToken);

                //if (!isAuthenticated)
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new JsonResult(new Response<bool> { IsError = true, Message = "Access Denied!", Data = false });
                    return;
                }

                //var isAccountVerify = userManagementService.isEmailValid(Convert.ToInt32(tokenData.UserId));
                //if (!isAccountVerify)
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new JsonResult(new Response<bool> { IsError = true, Message = "Please verify your account first.", Data = false });
                    return;
                }
            }
            else
            {
                context.RouteData.Values.Add("userId", Guid.Empty.ToString());
                context.RouteData.Values.Add("DeviceToken", "");
            }
        }
    }
}
