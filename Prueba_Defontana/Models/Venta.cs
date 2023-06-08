namespace Prueba_Defontana
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Venta")]
    public partial class Venta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Venta()
        {
            VentaDetalle = new HashSet<VentaDetalle>();
        }

        [Key]
        public long ID_Venta { get; set; }

        public int Total { get; set; }

        public DateTime Fecha { get; set; }

        public long ID_Local { get; set; }

        public virtual Local Local { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VentaDetalle> VentaDetalle { get; set; }
    }
}
