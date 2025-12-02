using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion.DTOs.Pedido
{
    public class PedidoUpdateRequestDto
    {
        [Required]

        public DateTime Fecha { get; set; }
        [Required]

        public decimal Total { get; set; }

    }
}
