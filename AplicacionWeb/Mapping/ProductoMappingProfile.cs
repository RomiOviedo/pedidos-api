using AutoMapper;
using Pedidos.Aplicacion.DTOs.Producto;
using Pedidos.Entidades;

namespace Pedidos.AplicacionWeb.Mapping
{
    public class ProductoMappingProfile : Profile
    {
        public ProductoMappingProfile()
        {
            CreateMap<ProductoCreateRequestDto, Producto>()
         .ForMember(dest => dest.Stock, opt => opt.Ignore()).AfterMap((dto, categoria) =>
          { categoria.SetStock(dto.Stock); })


         .ForMember(dest => dest.Precio, opt => opt.Ignore()).AfterMap((dto, categoria) =>
          { categoria.SetPrecio(dto.Precio); })

         .ForMember(dest => dest.Nombre, opt => opt.Ignore()).AfterMap((dto, categoria) =>
          { categoria.SetNombre(dto.Nombre); });


            CreateMap<ProductoUpdateRequestDto, Producto>();

            CreateMap<Producto, ProductoResponseDto>();



        }
    }
}
