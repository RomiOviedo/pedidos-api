using System.ComponentModel.DataAnnotations;

namespace Pedidos.Aplicacion.DTOS.Cliente
{
    public class ClienteUpdateRequestDto
    {
        //public int Id { get; set; }

        [StringLength(30)]
        public string Nombre { get; set; }

        [StringLength(30)]
        public string Apellido { get; set; }

        [Phone]
        [StringLength(15)]
        public string Telefono { get; set; }

        [StringLength(50)]
        public string Direccion { get; set; }

    }
}
