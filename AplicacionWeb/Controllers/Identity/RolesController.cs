using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Aplicacion.DTOs.Identity.Roles;
using Pedidos.Entidades.MicrosoftIdentity;

namespace Pedidos.AplicacionWeb.Controllers.Identity
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController:ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<CategoriasController> _logger;
        private readonly IMapper _mapper;

        public RolesController(RoleManager<Role> roleManager, ILogger<CategoriasController>logger, IMapper mapper)
        {
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        //[Authorize(Roles = "Administrador, Encargado")]

        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<RoleResponseDto>>(_roleManager.Roles.ToList()));

            //var roles = _roleManager.Roles.ToList();
            //return Ok(_mapper.Map<IList<RoleResponseDto>>(roles));
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Guardar(RoleRequestDto roleRequestDto)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirst("Id")?.Value);

                try
                {
                    var role = _mapper.Map<Role>(roleRequestDto);
                    role.Id = Guid.NewGuid();
                    var result = _roleManager.CreateAsync(role).Result;
                    if (result.Succeeded)
                    {
                        return Ok(role.Id);
                    }
                    return Problem(detail: result.Errors.First().Description, instance: role.Name, statusCode: StatusCodes.Status409Conflict);
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
            else
            {
                return BadRequest("los datos enviados no son validos.");
            }
        }



        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Administrador")]

        public IActionResult Modificar([FromBody] RoleRequestDto roleRequestDto, [FromQuery] Guid id)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirst("Id")?.Value);

                try
                {
                    var role = _mapper.Map<Role>(roleRequestDto);

                    role.Id = id;

                    var result = _roleManager.UpdateAsync(role).Result;

                    if (result.Succeeded)
                    {
                        return Ok(role.Id);
                    }
                    return Problem(detail: result.Errors.First().Description, instance: role.Name, statusCode: StatusCodes.Status409Conflict);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return BadRequest("Los datos enviados no son validos.");
            }
        }


       // [Route("GetById")]

        [Route("ById/{id}")]
        [HttpGet]
        [Authorize(Roles = "Administrador, Encargado")]

        public IActionResult GetById(Guid? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    return BadRequest();
                }
                var role = _roleManager.FindByIdAsync(id.Value.ToString());

                if (role==null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<RoleResponseDto>(role));
            }
            catch (Exception ex)
            {

                return Conflict();
            }

        }



    }



}
