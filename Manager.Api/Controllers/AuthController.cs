using Manager.Api.Controllers.Token;
using Manager.Api.Controllers.ViewModel;
using Manager.Api.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Api.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        [Route("/api/v1/auth/login")]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                var tokenLogin = _configuration["JWT:Login"];
                var tokenPassword = _configuration["JWT:Password"];
                
                if(loginViewModel.Login.Equals(tokenLogin) && loginViewModel.Password.Equals(tokenPassword))
                {
                    return Ok(new ResultViewModel
                    {
                        Message = "Usuario Autenticado com Sucesso!",
                        Success = true,
                        Data = new
                        {
                            Token = _tokenGenerator.GenerateToken(),
                            TokenExpires = DateTime.UtcNow.AddHours(int.Parse(_configuration["JWT:HoursToExpire"])),
                        }
                    });
                }
                else
                {
                    return StatusCode(401, Responses.UnauthorizedErrorMessage());
                }
                
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }
    }
}
