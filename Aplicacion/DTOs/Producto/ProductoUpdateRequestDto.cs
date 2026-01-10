using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion.DTOs.Producto
{
    public class ProductoUpdateRequestDto
    {



        [Required]
        [StringLength(30)]
        public string Nombre { get; set; } = null!;


        [Required]

        [Range(0, double.MaxValue, ErrorMessage = "No se aceptan valores negativos")]
        public decimal Precio { get; set; }


        [Required]

        [Range(0, int.MaxValue, ErrorMessage = "El Stock no puede ser nagativo")]
        public int Stock { get; set; }


        [Required]

        public int IdCategoria { get; set; }

    }
}
