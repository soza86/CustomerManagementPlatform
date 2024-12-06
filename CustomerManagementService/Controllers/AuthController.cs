using CustomerManagementService.BusinessLayer;
using CustomerManagementService.Models.Resources;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public AuthController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel model)
        {
            var result = await _userManagementService.LoginUser(model);
            if (string.IsNullOrEmpty(result))
                return Unauthorized();

            return Ok(result);
        }
    }
}
