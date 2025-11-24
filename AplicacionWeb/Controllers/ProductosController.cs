using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Aplicacion;
using Pedidos.Aplicacion.DTOs.Producto;
using Pedidos.Entidades;
using Pedidos.Entidades.MicrosoftIdentity;

namespace Pedidos.AplicacionWeb.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [Route("api/[controller]")]
    [ApiController]

    public class ProductosController:ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly ILogger<ProductosController> _logger;
        private readonly IApplication<Producto> _producto;
        private readonly IMapper _mapper;

        public ProductosController(ILogger<ProductosController> logger, IApplication<Producto> Producto, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _producto = Producto;
            _mapper = mapper;
            _userManager = userManager;

        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Administrador, Encargado,Cajero ")]

        public async Task<IActionResult> All()
        {
            var producto = _producto.GetAll();

            if (producto == null || !producto.Any())
            {
                // return Ok(new List<ClienteResponseDto>());  // Devuelve lista vacía
                // también podrías usar:
                return NoContent();

            }

            return Ok(_mapper.Map<IList<ProductoResponseDto>>(producto));

        }


        [HttpGet("ById/{id}")]
        // [Route("ById")]
        [Authorize(Roles = "Administrador, Encargado, Cajero")]

        public async Task<IActionResult> ById(int id)
        {
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }

                var Producto = _producto.GetById(id);

                if (Producto == null)
                {
                    return NotFound($"No se encontró un Producto con Id = {id}");
                }

                return Ok(_mapper.Map<ProductoResponseDto>(Producto));
            }
        }

        [HttpPost] //CREAR
        [Authorize(Roles = "Administrador, Encargado")]

        public async Task<IActionResult> Crear(ProductoCreateRequestDto ProductoRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var Producto = _mapper.Map<Producto>(ProductoRequestDto);

            _producto.Save(Producto);

            //return Ok(_mapper.Map<ProductoResponseDto>(Producto));
            return Ok(Producto.Id);
        }


        [HttpPut("{Id}")] //EDITAR
        [Authorize(Roles = "Administrador, Encargado")]

        public async Task<IActionResult> Editar(int? Id, ProductoUpdateRequestDto ProductoUpdateRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Producto ProductoBack = _producto.GetById(Id.Value);

            //Producto ProductoBack = _producto.GetById(Id.Value);

            if (ProductoBack is null)
            {
                return NotFound();
            }

            //ProductoBack = _mapper.Map<Producto>(ProductoUpdateRequestDto);
            _mapper.Map(ProductoUpdateRequestDto, ProductoBack);

            _producto.Save(ProductoBack);

            return Ok(_mapper.Map<ProductoResponseDto>(ProductoBack));


        }

        [HttpDelete("{Id}")]
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
            Producto ProductoBack = _producto.GetById(Id.Value);

            if (ProductoBack is null)
            {
                return NotFound();
            }
            _producto.Delete(ProductoBack.Id);
            return Ok($"Se eliminó el producto con ID: {ProductoBack.Id}");
        }


    }



}

