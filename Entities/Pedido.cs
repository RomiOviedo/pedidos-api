using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Pedidos.Abstraccion;

namespace Pedidos.Entidades
{
    public class Pedido : IEntidad
    {
        public Pedido()
        {

        }
        public Pedido(DateTime fecha, decimal total)
        {
            SetFecha(fecha);
            SetTotal(total);
        }
        #region Properties
        public int Id { get; set; }


        [ForeignKey(nameof(Cliente))]
        public int IdCliente { get; set; }
        [Required]

        public DateTime Fecha { get; private set; }

        [Required]

        public decimal Total { get; private set; }

        #endregion
        #region Virtual
        public virtual Cliente Cliente { get; set; }
        #endregion


        #region Setter y Getter

        public void SetFecha(DateTime fecha)
        {
            if (fecha.Date< DateTime.Today )
            {
                throw new ArgumentException("la fecha del pedido debe ser igual o mayor a la fecha actual ");
            }
            Fecha = fecha;
        }

        public void SetTotal(decimal total)
        {
            if (total<0)
            {
                throw new ArgumentException("el total debe ser mayor a 0");
            }
            Total = total;

        }


        #endregion
    }
}
