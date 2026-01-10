using AutoMapper;
using Pedidos.Entidades.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Entidades;
using Pedidos.Aplicacion;
using Pedidos.Aplicacion.DTOs.DetallePedido;

namespace DetallesPedidos.AplicacionWeb.Controller
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DetallesPedidosController:ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly ILogger<DetallesPedidosController> _logger;
        private readonly IApplication<DetallePedido> _detallePedido;
        private readonly IMapper _mapper;

        public DetallesPedidosController(ILogger<DetallesPedidosController> logger, IApplication<DetallePedido> detallePedido, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _detallePedido = detallePedido;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Administrador, Encargado,Cajero ")]
        public async Task<IActionResult> All()
        {
            var deteallePedidos = _detallePedido.GetAll();

            if (deteallePedidos == null || !deteallePedidos.Any())
            {
                return NoContent();

            }

            return Ok(_mapper.Map<IList<DetallePedidoResponseDto>>(_detallePedido.GetAll()));

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

                var detallPedido = _detallePedido.GetById(id);

                if (detallPedido == null)
                {
                    return NotFound($"No se encontró un Detalle de Pedido con Id = {id}");
                }

                return Ok(_mapper.Map<DetallePedidoResponseDto>(detallPedido));
            }
        }
        [HttpPost] 
        [Authorize(Roles = "Administrador, Encargado, Cajero")]

        public async Task<IActionResult> Crear(DetallePedidoRequestDto detallePedidoRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var detallePedido = _mapper.Map<DetallePedido>(detallePedidoRequestDto);

            _detallePedido.Save(detallePedido);

            return Ok(detallePedido.Id);

        }



        [HttpPut] 
        [Authorize(Roles = "Administrador, Encargado")]

        public async Task<IActionResult> Editar(int? Id, DetallePedidoRequestDto detallePedidoUpdateRequestDto)
        {

            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            DetallePedido pedidoBack = _detallePedido.GetById(Id.Value);

            if (pedidoBack is null)
            {
                return NotFound();
            }

            _mapper.Map(detallePedidoUpdateRequestDto, pedidoBack);



            _detallePedido.Save(pedidoBack);

            return Ok(_mapper.Map<DetallePedidoResponseDto>(pedidoBack));

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
            DetallePedido detallePedidoBack = _detallePedido.GetById(Id.Value);

            if (detallePedidoBack is null)
            {
                return NotFound();
            }
            _detallePedido.Delete(detallePedidoBack.Id);
            return Ok($"Se eliminó el detalle del pedido con ID {detallePedidoBack.Id}");
        }



    }
}
