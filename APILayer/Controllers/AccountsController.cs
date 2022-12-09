using BusinessLogicLayer;
using BusinessLogicLayer.Interface;
using GlobalEntityLayer.Models.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carfluent.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccounts _accounts;

        public AccountsController(IAccounts accounts) 
        {
            _accounts = accounts;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel registerModel)
        {
            var result = await _accounts.SignUpAsync(registerModel);
            if(result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();  
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var result = await _accounts.LoginAsync(loginModel);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
