using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion.DTOs.Categoria
{
    public class CategoriaUpdateRequestDto
    {

        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }


    }
}
