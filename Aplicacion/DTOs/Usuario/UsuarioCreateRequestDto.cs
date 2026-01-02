using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion.DTOs.Usuario
{
    public class UsuarioCreateRequestDto
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }


    }
}
