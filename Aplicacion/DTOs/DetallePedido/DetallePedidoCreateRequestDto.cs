using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion.DTOs.DetallePedido
{
    public class DetallePedidoCreateRequestDto
    {
        [Required]
        public int IdPedido { get; set; }


        [Required]
        public int IdProducto { get; set; }


        [Range(1, int.MaxValue, ErrorMessage ="la cantidad debe ser mayor a cero ")]
        public int Cantidad { get; set; }


        [Range(0, double.MaxValue, ErrorMessage ="El precio no puede ser negativo")]
        public decimal PrecioUnitario { get; set; }


    }
}
