using AutoMapper;
using Pedidos.Aplicacion.DTOs.Categoria;
using Pedidos.Entidades;

namespace Pedidos.AplicacionWeb.Mapping
{
    public class CategoriaMappingProfile:Profile
    {
        public CategoriaMappingProfile()
        {
            CreateMap<Categoria, CategoriaResponseDto>();


            CreateMap<CategoriaCreateRequestDto, Categoria>();
               

            CreateMap<CategoriaUpdateRequestDto, Categoria>()
                .ForMember(dest => dest.Nombre, opt => opt.Ignore())
                .AfterMap((dto, categoria) =>
                {
                    categoria.SetNombre(dto.Nombre);
                });

        }
    }
}
