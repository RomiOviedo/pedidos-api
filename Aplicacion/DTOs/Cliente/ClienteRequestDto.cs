using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion.DTOs.Cliente
{
    public class ClienteRequestDto
    {
        [Required]

        [StringLength(30)]
        public string Nombre { get; set; } = null!;
        [Required]

        [StringLength(30)]
        public string Apellido { get; set; } = null!;
        [Required]

        [Phone]
        [StringLength(15)]
        public string Telefono { get; set; }
        [Required]

        [StringLength(50)]
        public string Direccion { get; set; } = null!;

    }
}
