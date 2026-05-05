using HocGadgetShopAPI.Models.Dtos.Auth;
using HocGadgetShopAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HocGadgetShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            try
            {
                var token = await _service.Register(request);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); 
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var token = await _service.Login(request);
            return Ok(new { token });
        }
    }
}
