using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion.DTOs.Pedido
{
    public class PedidoResponseDto
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

    }
}
