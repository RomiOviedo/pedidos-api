using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion.DTOs.Pedido
{
    public class PedidoUpdateRequestDto
    {
       // public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

    }
}
