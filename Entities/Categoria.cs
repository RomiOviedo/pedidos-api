using Pedidos.Abstraccion;
using System.ComponentModel.DataAnnotations;

namespace Pedidos.Entidades
{
    public class Categoria : IEntidad
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }
        public Categoria(string nombre)
        {
            SetNombre(nombre);
        }

        #region Properties

        public int Id { get ; set ; }

        [Required]
        [StringLength(30)]
        public string Nombre { get; private set; }
        #endregion

        #region Virtual

        public virtual ICollection<Producto> Productos { get; set; }
        #endregion




        #region setters y getters
        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la categoria no puede estar vacío.");
            Nombre = nombre;
        }
        public string GetName()
        {
            return string.Join(": ", this.GetName(), Nombre);
        }

        public string GetClassName()
        {
            return string.Join(": ", this.GetType().BaseType.Name, Nombre);
        }

        #endregion



    }
}
