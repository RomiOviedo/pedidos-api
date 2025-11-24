using AutoMapper;
using Pedidos.Aplicacion.DTOs.Pedido;
using Pedidos.Entidades;

namespace Pedidos.AplicacionWeb.Mapping
{
    public class PedidoMappingProfile:Profile
    {
        public PedidoMappingProfile()
        {
            CreateMap<Pedido, PedidoResponseDto>().
                ForMember(dest => dest.Fecha, ori => ori.MapFrom(src=>src.Fecha.ToShortDateString()));

            CreateMap<PedidoCreateRequestDto,Pedido>()
           .ForMember(dest => dest.Fecha, opt => opt.Ignore()).AfterMap((dto, categoria) =>
            { categoria.SetFecha(dto.Fecha); })

            .ForMember(dest => dest.Total, opt => opt.Ignore()).AfterMap((dto, categoria) =>
            { categoria.SetTotal(dto.Total); });



            CreateMap<PedidoUpdateRequestDto, Pedido>();

        }
    }
}
