using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Aplicacion;
using Pedidos.Aplicacion.DTOs.Categoria;
using Pedidos.Entidades;
using Pedidos.Entidades.MicrosoftIdentity;

namespace Pedidos.AplicacionWeb.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriasController : ControllerBase
    {


        private readonly UserManager<User> _userManager;
        private readonly ILogger<CategoriasController> _logger;
        private readonly IApplication<Categoria> _categoria;
        private readonly IMapper _mapper;

        public CategoriasController(ILogger<CategoriasController> logger, UserManager<User> userManager, IApplication<Categoria> Categoria, IMapper mapper)
        {
            _logger = logger;
            _categoria = Categoria;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("All")]
        //[Authorize(Roles = "Administrador, Encargado, Cajero")]

        public async Task<IActionResult> All()
        {


              return Ok(_mapper.Map<IList<CategoriaResponseDto>>(_categoria.GetAll()));

        }



        [HttpGet("ById/{id}")]
        [Authorize(Roles = "Administrador, Encargado, Cajero")]
        public async Task<IActionResult> ById(int id)
        {
            Categoria categoria = _categoria.GetById(id);

            if (categoria is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoriaResponseDto>(categoria));
        }

        [HttpPost] 
        [Authorize(Roles = "Administrador, Encargado")]


        public async Task<IActionResult> Crear(CategoriaRequestDto CategoriaRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var Categoria = _mapper.Map<Categoria>(CategoriaRequestDto);



            _categoria.Save(Categoria);

            return Ok(Categoria.Id);
        }


        [HttpPut]
        [Authorize(Roles = "Administrador,Encargado")]

        public async Task<IActionResult> Editar(int? Id, [FromBody] CategoriaRequestDto CategoriaUpdateRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Categoria CategoriaBack = _categoria.GetById(Id.Value);


            if (CategoriaBack is null)
            {
                return NotFound();
            }


            try
            {
                _mapper.Map(CategoriaUpdateRequestDto, CategoriaBack);

                _categoria.Save(CategoriaBack);

                return Ok();

            }
            catch (ArgumentException aex)
            {
                return BadRequest(aex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al editar categoría", Error = ex.Message });
            }
        }



        [HttpDelete]
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Categoria CategoriaBack = _categoria.GetById(Id.Value);

            if (CategoriaBack is null)
            {
                return NotFound();
            }
            _categoria.Delete(CategoriaBack.Id);
             return Ok();
        }





    }
}

