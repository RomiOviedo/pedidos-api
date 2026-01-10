using AutoMapper;
using Pedidos.Entidades.MicrosoftIdentity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Entidades;
using Pedidos.Aplicacion;
using Pedidos.Aplicacion.DTOs.Cliente;
using Pedidos.Aplicacion.DTOS.Cliente;

namespace Pedidos.AplicacionWeb.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]


    public class ClientesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly ILogger<ClientesController> _logger;
        private readonly IApplication<Cliente> _cliente;
        private readonly IMapper _mapper;   

        public ClientesController(ILogger<ClientesController> logger, IApplication<Cliente> cliente, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _cliente = cliente;
            _mapper = mapper;
            _userManager = userManager; 

        }

        [HttpGet]
        [Route("All")]
        [Authorize(Roles = "Administrador, Encargado,Cajero ")]

        public async Task<IActionResult> All()
        {
            var clientes = _cliente.GetAll();

            if (clientes == null || !clientes.Any())
            {
                return NoContent();

            }

            return Ok(_mapper.Map<IList<ClienteResponseDto>>(clientes));


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

                var cliente = _cliente.GetById(id);

                if (cliente == null)
                {
                    return NotFound($"No se encontró un cliente con Id = {id}");
                }

                return Ok(_mapper.Map<ClienteResponseDto>(cliente));
            }
        }

       
        
        
        [HttpPost] 
        [Authorize(Roles = "Administrador, Encargado,Cajero ")]

        public async Task<IActionResult> Crear(ClienteRequestDto clienteRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var cliente = _mapper.Map<Cliente>(clienteRequestDto);

            _cliente.Save(cliente);

            return Ok(cliente.Id);
        }


        [HttpPut] 
        [Authorize(Roles = "Administrador, Encargado, Cajero")]

        public async Task<IActionResult> Editar(int? Id, ClienteRequestDto clienteUpdateRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


           Cliente clienteBack = _cliente.GetById(Id.Value);

            if (clienteBack is null)
            {
                return NotFound();
            }

            _mapper.Map(clienteUpdateRequestDto, clienteBack);

            _cliente.Save(clienteBack);

            return Ok(_mapper.Map<ClienteResponseDto>(clienteBack));


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
            Cliente clienteBack = _cliente.GetById(Id.Value);

            if (clienteBack is null)
            {
                return NotFound();
            }
            _cliente.Delete(clienteBack.Id);
            return Ok($"Se eliminó el cliente con ID: {clienteBack.Id}");
        }


    }


}
