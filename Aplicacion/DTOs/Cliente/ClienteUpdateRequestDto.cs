using System.ComponentModel.DataAnnotations;

namespace Pedidos.Aplicacion.DTOS.Cliente
{
    public class ClienteUpdateRequestDto
    {
        [Required]

        [StringLength(30)]
        public string Nombre { get; set; }
        [Required]

        [StringLength(30)]
        public string Apellido { get; set; }
        [Required]

        [Phone]
        [StringLength(15)]
        public string Telefono { get; set; }
        [Required]

        [StringLength(50)]
        public string Direccion { get; set; }

    }
}
