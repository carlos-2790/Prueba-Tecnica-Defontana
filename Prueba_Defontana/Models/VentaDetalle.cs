namespace Prueba_Defontana
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VentaDetalle")]
    public partial class VentaDetalle
    {
        [Key]
        public long ID_VentaDetalle { get; set; }

        public long ID_Venta { get; set; }

        public int Precio_Unitario { get; set; }

        public int Cantidad { get; set; }

        public int TotalLinea { get; set; }

        public long ID_Producto { get; set; }

        public virtual Producto Producto { get; set; }

        public virtual Venta Venta { get; set; }
    }
}
