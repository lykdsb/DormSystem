using System;
using Microsoft.AspNetCore.Mvc;
using backend.Configs;
using backend.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController:ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Submit([FromBody]User userInput)
        {
            var userID = userInput.UserID;
            var password = userInput.Password;
            User user;
            try
            {
                user = DBContext.DBstatic.Queryable<User>().Single(c => c.UserID == userID);
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    success =0,
                    msg= e.Message
                }
                );
            }
            if (user == null || !password.Equals(user.Password))
            {
                return Ok(new
                {
                    success = 0,
                    msg = "Wrong userID or password"
                }) ;
            }
            else
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{ new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                    new Claim(JwtRegisteredClaimNames.Exp,$"{ new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds() }"),
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userID))//自定义的 payload 部分，包含了用户的 ID 用于识别身份
                };
                //生成密钥用于签名
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalVar.Secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //生成 token
                var token = new JwtSecurityToken(
                    issuer: GlobalVar.Domain,
                    audience: GlobalVar.Domain,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);
                //返回 token 给客户端使用
                return Ok(new
                {
                    success =1,
                    token=new JwtSecurityTokenHandler().WriteToken(token)
                }
                ) ;
            }
        }
    }
}
