using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.API.Controllers.V1
{
    //[Route("api/[controller]")]
    [Route("api/Login")]
    [ControllerName("LoginV1")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiController]
    //[ApiExplorerSettings(IgnoreApi = true)]// Hiding Login Api from Swagger documenation 
    public class LoginV1Controller : ControllerBase
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public LoginV1Controller(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            this._configuration = configuration;
        }



        [AllowAnonymous]
        //[HttpPost("Authenticate")]
        //[ValidateModel]
        [HttpPost]
        public IActionResult Authenticate(string username,string password)
        {
            Helpers.StatusResult<string> status = new Helpers.StatusResult<string>();
            var response = GenerateJwtToken(1,1, "admin");
            status.Message = "Login Successful";
            status.Result = response;
            status.Status = Portfolio.API.Helpers.ResponseStatus.LoginSuccess;
            return Ok(status);
        }
        private string GenerateJwtToken(int UserId,int RoleId,string RoleName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(/*_appSettings.SecretKey*/"c59af8dc897421c5b16fae2d4c27736c9be0aba0cdf105bbabdcccfc75412c17");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "https://localhost:44391/",/*_appSettings.Issuer*/
                Audience = "https://localhost:44391/", /*_appSettings.Audience*/
                IssuedAt = DateTime.Now,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("uid",UserId.ToString()),
                    new Claim("roleId",RoleId.ToString()),
                    new Claim("role",RoleName),
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(/*_appSettings.Expires*/"30")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
