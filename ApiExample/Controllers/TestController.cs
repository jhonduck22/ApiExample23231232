using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ApiExample.Controllers
{
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var now = DateTime.UtcNow;
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123124124124124124122141242421421rf1f2qr2f24234234234234234234234324234234234234234234234234234234234234234234234234234234234234"))
                    , SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Role, "Administrator")
                },
                notBefore: now,
                expires: now.AddMinutes(200),
                signingCredentials: signingCredentials));

            return Ok(accessToken);
        }

        [HttpGet]
        public IActionResult ActionTest()
        {
            return Ok("Unregistered Response");
        }

        [HttpGet]
        [Authorize(Policy = "AdministratorOnly")]
        public IActionResult IdentityTestToken()
        {
            return Ok("Administrator Response");
        }

        [HttpGet]
        [Authorize(Policy = "TestingIdentityOnly")]
        public IActionResult IdentityTestStore()
        {
            return Ok("UerStore Response");
        }
    }
}
