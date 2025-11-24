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
                //.ForMember(dest=> dest.Nombre, ori=> ori.MapFrom(src=>src.Nombre));





            CreateMap<CategoriaCreateRequestDto, Categoria>();
               
                //.ConstructUsing(dto=> new Categoria(dto.Nombre)); //para crear una categoria nueva


            CreateMap<CategoriaUpdateRequestDto, Categoria>()
                .ForMember(dest => dest.Nombre, opt => opt.Ignore())
                .AfterMap((dto, categoria) =>
                {
                    categoria.SetNombre(dto.Nombre);
                });


            //            ¿Qué es ForMember?

            //ForMember sirve para configurar cómo AutoMapper debe mapear un miembro(propiedad) específico del destino.

            //Lo podés usar para:

            //Ignorar una propiedad

            //Mapearla desde otra propiedad

            //Aplicar transformaciones

            //Cambiar nombres

            //✔ Ejemplo:
            //.ForMember(dest => dest.Nombre, opt => opt.Ignore())


            //👉 Esto le dice a AutoMapper:

            //“No toques la propiedad Nombre del objeto destino”.

            //¿Por qué lo ignoramos?
            //Porque Nombre es privado(private set) y SOLO se puede modificar usando tu método SetNombre().
            //Entonces, AutoMapper no puede asignarlo directamente → lo ignoramos.

            //✅ ¿Qué es AfterMap?

            //AfterMap es un callback que se ejecuta después de que AutoMapper terminó el mapeo.
            //Sirve para aplicar lógica adicional.

            //En tu caso lo usamos para llamar tu método de dominio, que sí puede modificar Nombre:

            //.AfterMap((dto, categoria) =>
            //{
            //            categoria.SetNombre(dto.Nombre);
            //        })


            //👉 Esto le dice a AutoMapper:

            //“Una vez que terminaste de mapear, llamá al método SetNombre() y aplicá la validación correspondiente”.

            //🔍 ¿Por qué necesitamos ambos al mismo tiempo?

            //Porque:

            //AutoMapper no puede asignar Nombre directamente
            //→ entonces lo ignoramos con ForMember(...Ignore())

            //Pero igual queremos actualizar Nombre
            //→ entonces lo hacemos manualmente en AfterMap
        }
    }
}
