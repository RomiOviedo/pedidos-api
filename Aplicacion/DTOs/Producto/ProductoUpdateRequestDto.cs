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

      //  public int Id { get; set; }


        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }



        [Range(0, double.MaxValue, ErrorMessage = "No se aceptan valores negativos")]
        public decimal Precio { get; set; }



        [Range(0, int.MaxValue, ErrorMessage = "El Stock no puede ser nagativo")]
        public int Stock { get; set; }



        [ForeignKey(nameof(Categoria))]
        public int IdCategoria { get; set; }

    }
}
