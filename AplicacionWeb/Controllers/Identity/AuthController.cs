using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Aplicacion.DTOs.Identity.User;
using Pedidos.Aplicacion.DTOs.Login;
using Pedidos.AplicacionWeb.Configurations;
using Pedidos.Entidades.MicrosoftIdentity;
using Pedidos.Servicios.AuthServices;

namespace Pedidos.AplicacionWeb.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController:ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<CategoriasController> _logger;
        private readonly ITokenHandlerService _servicioToken;
        public AuthController(UserManager<User> userManager, ILogger<CategoriasController> logger, ITokenHandlerService servicioToken)
        {
            _userManager = userManager;
            _logger = logger;
            _servicioToken = servicioToken;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UserRegistroRequestDto user)
        {
            if (ModelState.IsValid)
            {
                var existeUsuario = await _userManager.FindByEmailAsync(user.Email);


                 
                if (existeUsuario!=null)
                {
                  return  BadRequest("Ya existe un usuario registrado con el mail:"+user.Email);
                }

                var crear = await _userManager.CreateAsync(new User()

                {
                    Email = user.Email,
                    UserName = user.Email.Substring(0, user.Email.IndexOf("@")),//Busca en qué posición está el @
                    Nombres = user.Nombres,
                    Apellidos = user.Apellidos,
                    FechaNacimiento = user.FechaNacimiento
                },
                user.Password
                );

                if (crear.Succeeded)
                {
                    return Ok(new UserRegistroResponseDto
                    {
                        NombreCompleto = string.Join("", user.Nombres, user.Apellidos),
                        Email = user.Email,
                        UserName = user.Email.Substring(0, user.Email.IndexOf('@'))// Toma el texto desde la posición 0 hasta antes del @

                    }
                    );

                }
                else
                {
                    return BadRequest(crear.Errors.Select(e => e.Description).ToList());
                }


            }
            else
            {
                return BadRequest("Los datos enviados son invalidos");
            }


        }
        //[HttpPost]
        //[Route("RegisterSincronico")]

        //public IActionResult RegistrarUsuarioSincronico([FromBody] UserRegistroRequestDto user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existeUsuario = _userManager.FindByEmailAsync(user.Email).Result;
        //        if (existeUsuario != null)
        //        {
        //            return BadRequest("Existe un usuario registrado con el mal " + user.Email + ".");
        //        }
        //        var Creado = _userManager.CreateAsync(new User()
        //        {
        //            Email = user.Email,
        //            UserName = user.Email.Substring(0, user.Email.IndexOf('@')),
        //            Nombres = user.Nombres,
        //            Apellidos = user.Apellidos,
        //            FechaNacimiento = user.FechaNacimiento
        //        }, user.Password).Result;
        //        if (Creado.Succeeded)
        //        {
        //              var userBack= _userManager.FindByEmailAsyng(user.Email);
        //      _userManager.AddToRoleAsync(userBack.Result, "Administrador");
        //            return Ok(new UserRegistroResponseDto
        //            {
        //                NombreCompleto = string.Join(" ", user.Nombres, user.Apellidos),
        //                Email = user.Email,
        //                UserName = user.Email.Substring(0, user.Email.IndexOf('@'))
        //            });
        //        }
        //        else
        //        {
        //            return BadRequest(Creado.Errors.Select(e => e.Description).ToList());
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Los datos enviados no son validos.");
        //    }

        //}
        [HttpPost]
        [Route("login")]
        [AllowAnonymous] //permitir anonimo
        public async Task<IActionResult> Login([FromBody]
        LoginUserRequesDto userlogin)
        {
            if (ModelState.IsValid)
            {
                var existeUsuario = await _userManager.FindByEmailAsync(userlogin.Email);
                if (existeUsuario != null)
                {
                    var isCorrect = await _userManager.CheckPasswordAsync(existeUsuario, userlogin.Password);
                    if (isCorrect)
                    {
                        try
                        {

                            var roles = await _userManager.GetRolesAsync(existeUsuario);

                            var parametros = new TokenParameters()
                            {
                                Id = existeUsuario.Id.ToString(),
                                PaswordHash = existeUsuario.PasswordHash,
                                UserName = existeUsuario.UserName,
                                Email = existeUsuario.Email,
                                Roles = roles

                            };
                            var jwt = _servicioToken.GenerateJwtTokens(parametros);

                            return Ok(new LoginUserResponseDto()
                            {
                                Login = true,
                                Token = jwt,
                                UserName = existeUsuario.UserName,
                                Mail = existeUsuario.Email
                            });
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                }
            }
            return BadRequest(new LoginUserResponseDto()
            {
                Login=false,
                Errores=new List<string>()
                {
                    "Usuario o contraseña incorrecto!"
                }
            }
            );
        
        }
        
        
        
    }
}
