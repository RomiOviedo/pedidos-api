using AutoMapper;
using Pedidos.Aplicacion.DTOs.Usuario;
using Pedidos.Entidades.MicrosoftIdentity;

namespace Pedidos.AplicacionWebApi.Mapping
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UsuarioResponseDto>();

        }

    }
}
