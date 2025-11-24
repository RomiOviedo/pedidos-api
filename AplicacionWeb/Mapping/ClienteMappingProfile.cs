using AutoMapper;
using Pedidos.Aplicacion.DTOS.Cliente;
using Pedidos.Entidades;

namespace Pedidos.AplicacionWeb.Mapping
{
    public class ClienteMappingProfile:Profile
    {
        public ClienteMappingProfile()
        {
            CreateMap<Cliente, ClienteResponseDto>();

            CreateMap<ClienteUpdateRequestDto, Cliente>()
            .ForMember(dest => dest.Nombre, opt => opt.Ignore()).AfterMap((dto, categoria) =>
                {categoria.SetNombre(dto.Nombre);})

            .ForMember(dest => dest.Apellido, opt => opt.Ignore()).AfterMap((dto, categoria) =>
                { categoria.SetApellido(dto.Apellido);})

            .ForMember(dest => dest.Telefono, opt => opt.Ignore()).AfterMap((dto, categoria) =>
                {categoria.SetTelefono(dto.Telefono);});





            CreateMap<ClienteCreateRequestDto, Cliente>();

        }


    }
}
