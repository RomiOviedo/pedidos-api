using System.ComponentModel.DataAnnotations;

namespace Pedidos.Aplicacion.DTOs.Identity.User
{
    public class UserRegistroResponseDto
    {
       // [Required]
        public string NombreCompleto { get; set; } = null!;
        //  [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
       // [Required]
        public string UserName { get; set; } = null!;

    }
}
