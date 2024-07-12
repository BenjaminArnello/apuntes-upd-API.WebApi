using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Proyectos.DB;
using ProyectosArqui.Core;
using ProyectosArqui.Core.CustomExceptions;


namespace apuntes_upd_API.WebApi.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServices _authenticationServices;

        public AuthenticationController (IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }




        // POST api/<UserController>
        [HttpPost("registro")]
        public async Task<IActionResult> Registro(Usuarios user)
        {
            try
            {
                var result = await _authenticationServices.Registro(user);

                return Created("", result);
            }

            catch (UsuarioYaExisteException e)
            {
                return StatusCode(409, e.Message);
            }


        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ProyectosArqui.Core.DTO.ModeloLogin loginRequest)
        {
            try
            {
                var result = await _authenticationServices.Login(loginRequest.Mail, loginRequest.Clave);
                return Created("", result);
            }
            catch (CredencialesIncorrectasExcepción e)
            {
                return StatusCode(401, e.Message);
            }
        }



    }
}
