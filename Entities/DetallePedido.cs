using Pedidos.Abstraccion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Entidades
{
    [Table("DetallesPedidos")] // le decís a EF cómo se llama la tabla real
    public class DetallePedido : IEntidad
    {
        public DetallePedido()
        {

        }

        public DetallePedido(int cantidad, decimal precioUnit)
        {
            SetCantidad(cantidad);
            SetPrecioUnitario(precioUnit);
            SetSubtotal();

        }
        #region Properties

        public int Id { get; set; }

        [ForeignKey(nameof(Pedido))]
        public int IdPedido { get; set; }


        [ForeignKey(nameof(Producto))]
        public int IdProducto { get; set; }
        [Required]

        public int Cantidad { get; private set; } 
        [Required]

        public decimal PrecioUnitario { get; private set; } 

        [Required]

        public decimal Subtotal { get; private set; }
        #endregion


        #region Virtual

        public virtual Pedido Pedido { get; set; }

        public virtual Producto Producto { get; set; }
        #endregion

        #region Setter y Getter


        public void SetCantidad(int cantidad)
        {
            if (cantidad<0)
            {
                throw new ArgumentException("la cantidad debe ser mayor a 0");
            }
            Cantidad = cantidad;
        }

        public void SetPrecioUnitario(decimal precioUnit)
        {
            if (precioUnit < 0)
            {
                throw new ArgumentException("el precio unitario debe ser mayor a 0");
            }
            PrecioUnitario = precioUnit;
        }

        public void SetSubtotal()
        {
            if (Cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor a 0");
            }

            if (PrecioUnitario <= 0)
            {
                throw new ArgumentException("El precio debe ser mayor a 0");
            }

          Subtotal = PrecioUnitario * Cantidad;
        }


        #endregion
    }
}
