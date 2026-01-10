using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion.DTOs.Identity.User
{
    public class UserRegistroRequestDto
    {
        [Required]
        public string Nombres { get; set; } = null!;

        [Required]
        public string Apellidos { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }

    }
}
