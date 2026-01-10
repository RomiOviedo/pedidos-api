using Pedidos.Abstraccion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Entidades
{
    public class Producto : IEntidad
    {
        public Producto()
        {
                
        }
        public Producto(int stock, decimal precio, string nombre)
        {
            SetStock(stock);
            SetPrecio(precio);
            SetNombre(nombre);
        }
        #region Properties
        public int Id { get; set; }


        [Required]
        [StringLength(30)]
        public string Nombre { get; private set; } = null!;


        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "No se aceptan valores negativos")]
        public decimal Precio { get; private set; } 


        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El Stock no puede ser nagativo")]
        public int Stock { get; private set; }



        [ForeignKey(nameof(Categoria))]
        public int IdCategoria { get; set; }

        #endregion

        #region Vitual
        public virtual Categoria Categoria { get; set; }
        #endregion

        #region Setter y Getter
        public void SetStock(int stock)
        {
            if (stock < 0)
            {
                throw new ArgumentException("El stock no puede ser menor a 0");
            }
            Stock = stock;
        }
        public void SetPrecio(decimal precio)
        {
            if (precio < 0)
            {
                throw new ArgumentException("El precio no puede ser menor a 0");
            }
            Precio = precio;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("el nombre no puede estar vacio");

            }
            Nombre = nombre;
        }



        #endregion

    }
}
