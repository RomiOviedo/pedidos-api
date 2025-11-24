using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion.DTOs.DetallePedido
{
    public class DetallePedidoUpdateRequestDto
    {
        //[Required]
        //public int IdPedido { get; set; }

        [Required]
        public int IdProducto { get; set; }

        //[Required]
       // [Range(1, int.MaxValue, ErrorMessage ="la cantidad no puede ser negativa")]
        public int Cantidad { get; set; }


        //[Required]
        //[Range(0, double.MaxValue, ErrorMessage = "el precio no puede ser negativo")]

        public decimal PrecioUnitario { get; set; }

   

    }
}
