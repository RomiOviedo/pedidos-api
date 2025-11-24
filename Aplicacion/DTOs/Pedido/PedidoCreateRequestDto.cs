using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion.DTOs.Pedido
{
    public class PedidoCreateRequestDto
    {
        [Required]
        public int IdCliente { get; set; }


        [Required]
        [DataType(DataType.Date)]

        public DateTime Fecha { get; set; }

       [Required]
       [Range(0, double.MaxValue)]
        public decimal Total { get; set; }


    }
}
