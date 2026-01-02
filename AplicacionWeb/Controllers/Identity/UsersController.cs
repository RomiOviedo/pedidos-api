using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedidos.Aplicacion.DTOs.Categoria;
using Pedidos.Aplicacion.DTOs.Login;
using Pedidos.Aplicacion.DTOs.Usuario;
using Pedidos.Entidades;
using Pedidos.Entidades.MicrosoftIdentity;

namespace Pedidos.AplicacionWeb.Controllers.Identity
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]

    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(RoleManager<Role> roleManager, UserManager<User> userManager, ILogger<UsersController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;

        }

        [HttpGet]
        [Route("All")]
       // [Authorize(Roles = "Administrador, Encargado, Cajero")]
        public async Task<IActionResult> All()
        {


            var users = await _userManager.Users.ToListAsync();

            var dtoList = new List<UsuarioResponseDto>();

            foreach (var user in users)
            {
                UsuarioResponseDto usuario = new UsuarioResponseDto();
                usuario.Id = user.Id;
                usuario.Apellidos = user.Apellidos;
                usuario.Nombres = user.Nombres;
                usuario.Roles =string.Join(", " , _userManager.GetRolesAsync(user).Result);


                dtoList.Add(usuario);
            }
            return Ok(dtoList);

        }




        [HttpPost]
        [Route("AddRoleToUser")]
        [Authorize(Roles = "Administrador")]

        //  [AllowAnonymous] // Solo este endpoint puede ser usado sin token
        public async Task<IActionResult> Guardar(string userId, string roleId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            var role= _roleManager.FindByIdAsync(roleId).Result;
            
            if (user is not null && role is not null)

            {
                var status = await _userManager.AddToRoleAsync(user, role.Name);
               
                if (status.Succeeded)
                {
                    return Ok(new
                    {
                        user = user.UserName,
                        rol = role.Name
                    });
                }
            }
            return BadRequest(new
            {
                userId= userId,
                roleId = roleId


            });



        }







        [HttpDelete]
        [Route("DeleteToRole")]

        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> Borrar(string userId, string roleId)
        {

            var user = _userManager.FindByIdAsync(userId).Result;
            var role = _roleManager.FindByIdAsync(roleId).Result;

            if (user is not null && role is not null)

            {
                var status = await _userManager.RemoveFromRoleAsync(user, role.Name);

            }
            UsuarioResponseDto usuario = new UsuarioResponseDto();

            usuario.Apellidos = user.Apellidos;
            usuario.Nombres = user.Nombres;

            usuario.Roles = string.Join(", ", _userManager.GetRolesAsync(user).Result);


            return Ok(usuario);

        }



    }
}
