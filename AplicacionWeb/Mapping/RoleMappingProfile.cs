using AutoMapper;
using Pedidos.Aplicacion.DTOs.Identity.Roles;
using Pedidos.Entidades.MicrosoftIdentity;

namespace Pedidos.AplicacionWeb.Mapping
{
    public class RoleMappingProfile : Profile
    {


        public RoleMappingProfile()
        {
            CreateMap<Role, RoleResponseDto>();
            CreateMap<RoleRequestDto, Role>();
        }
    }
}
