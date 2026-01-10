using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Pedidos.Entidades.MicrosoftIdentity
{
    public class User : IdentityUser<Guid>
    {
        [Required(ErrorMessage = "{0} Required")]
        [StringLength(100)]
        [PersonalData]
        public string Nombres { get; set; } = null!;
        [Required(ErrorMessage = "{0} Required")]
        [StringLength(100)]
        [PersonalData]
        public string Apellidos { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }
    }
}
