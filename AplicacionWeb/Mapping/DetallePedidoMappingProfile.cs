using AutoMapper;
using Pedidos.Aplicacion.DTOs.DetallePedido;
using Pedidos.Entidades;

namespace Pedidos.AplicacionWeb.Mapping
{
    public class DetallePedidoMappingProfile:Profile
    {
        public DetallePedidoMappingProfile()
        {
            CreateMap<DetallePedido, DetallePedidoResponseDto>();

            CreateMap<DetallePedidoCreateRequestDto, DetallePedido>()
               .ForMember(dest => dest.Cantidad, opt => opt.Ignore()).AfterMap((dto, categoria) =>
                 { categoria.SetCantidad(dto.Cantidad); })

               .ForMember(dest => dest.PrecioUnitario, opt => opt.Ignore()).AfterMap((dto, categoria) =>
                 { categoria.SetPrecioUnitario(dto.PrecioUnitario); })

            .ForMember(dest => dest.Subtotal, opt => opt.Ignore()).AfterMap((dto, categoria) =>
            {
                categoria.SetCantidad(dto.Cantidad);
                categoria.SetPrecioUnitario(dto.PrecioUnitario);
                categoria.SetSubtotal();
            });


                CreateMap<DetallePedidoUpdateRequestDto, DetallePedido>()
                 .ForMember(dest => dest.Cantidad, opt => opt.Ignore())
                    .ForMember(dest => dest.PrecioUnitario, opt => opt.Ignore())
                    .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                    .AfterMap((dto, entidad) =>
                    {
                    entidad.SetCantidad(dto.Cantidad);
                    entidad.SetPrecioUnitario(dto.PrecioUnitario);
                    entidad.SetSubtotal();
                    });



        }
    }
}
