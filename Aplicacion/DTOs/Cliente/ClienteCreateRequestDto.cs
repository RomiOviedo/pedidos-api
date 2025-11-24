using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion.DTOS.Cliente
{
    public class ClienteCreateRequestDto
    {
        [StringLength(30)]
        public string Nombre { get; set; }

        [StringLength(30)]
        public string Apellido { get; set; }

        [Phone]
        [StringLength(15)]
        public string Telefono { get; set; }

        [StringLength(50)]
        public string Direccion { get; set; }

    }
}
