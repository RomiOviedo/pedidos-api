using Pedidos.Abstraccion;
using System.ComponentModel.DataAnnotations;

namespace Pedidos.Entidades
{
    public class Cliente : IEntidad
    {

        public Cliente()
        {

        }
        public Cliente(string nombre, string apellido, string telefono)
        {
            SetNombre(nombre);
            SetApellido(apellido);
            SetTelefono(telefono);
        }

        #region Properties

        public int Id { get; set; }
        [Required]

        [StringLength (30)]
        public string Nombre { get; private set; }
        [Required]

        [StringLength(30)]
        public string Apellido { get; private set; }
        [Required]

        [Phone]
        [StringLength(15)]
        public string Telefono { get; private set; }
        [Required]

        [StringLength(50)]
        public string Direccion { get; private set; }
        #endregion



        #region setters y getters
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del cliente no puede estar vacío.");
            Nombre = nombre;
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido del cliente no puede estar vacío.");
            Apellido = apellido;
        }
        public void SetTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("El telefono del cliente no puede estar vacío.");
            Telefono = telefono;


        }

        public string GetCompleteName()
        {
            return string.Join(", ", Apellido, Nombre);
        }
        #endregion

    }
}
