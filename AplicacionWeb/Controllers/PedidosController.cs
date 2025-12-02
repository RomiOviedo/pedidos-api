using AutoMapper;
using Pedidos.Entidades.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Aplicacion;
using Pedidos.Entidades;
using Pedidos.Aplicacion.DTOs.Pedido;

namespace Pedidos.AplicacionWeb.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]


    public class PedidosController:ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly ILogger<PedidosController> _logger;
        private readonly IApplication<Pedido> _pedido;
        private readonly IMapper _mapper;

        public PedidosController(ILogger<PedidosController> logger, IApplication<Pedido> pedido, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _pedido = pedido;
            _mapper = mapper;
            _userManager = userManager;

        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Administrador, Encargado,Cajero ")]

        public async Task<IActionResult> All()
        {
            var pedido = _pedido.GetAll();

            if (pedido == null || !pedido.Any())
            {
                return NoContent();

            }

            return Ok(_mapper.Map<IList<PedidoResponseDto>>(_pedido.GetAll()));

        }


        [HttpGet("ById/{id}")]
        [Authorize(Roles = "Administrador, Encargado, Cajero")]

        public async Task<IActionResult> ById(int id)
        {
            {
                if (id <= 0)
                {
                    return BadRequest("El id debe ser mayor a 0");
                }

                var pedido = _pedido.GetById(id);

                if (pedido == null)
                {
                    return NotFound($"No se encontró un Pedido con Id = {id}");
                }

                return Ok(_mapper.Map<PedidoResponseDto>(pedido));
            }
        }

        [HttpPost] 
        [Authorize(Roles = "Administrador, Encargado, Cajero")]

        public async Task<IActionResult> Crear(PedidoCreateRequestDto PedidoRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var Pedido = _mapper.Map<Pedido>(PedidoRequestDto);

            _pedido.Save(Pedido);

            return Ok(_mapper.Map<PedidoResponseDto>(Pedido));
        }


        [HttpPut("{Id}")] 
        [Authorize(Roles = "Administrador, Encargado")]

        public async Task<IActionResult> Editar(int? Id, PedidoUpdateRequestDto pedidoUpdateRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Pedido pedidoBack = _pedido.GetById(Id.Value);

        

            if (pedidoBack is null)
            {
                return NotFound();
            }

            _mapper.Map(pedidoUpdateRequestDto, pedidoBack);

            _pedido.Save(pedidoBack);

            return Ok(_mapper.Map<PedidoResponseDto>(pedidoBack));


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
            Pedido pedidoBack = _pedido.GetById(Id.Value);

            if (pedidoBack is null)
            {
                return NotFound();
            }
            _pedido.Delete(pedidoBack.Id);

         return Ok($"Se eliminó el pedido con ID {pedidoBack.Id}");
        }







    }
}
