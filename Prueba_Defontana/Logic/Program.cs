
using System;

using System.Linq;


namespace Prueba_Defontana
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new Model())
            {
                // Consultar todas las ventas de los últimos 30 días
                DateTime startDate = DateTime.Now.AddDays(-30);
                var ventas = dbContext.Venta
                    .Include("VentaDetalle")
                    .Where(v => v.Fecha >= startDate)
                    .ToList();

                // Total de ventas de los últimos 30 días
                decimal totalVentas = ventas.Sum(v => v.Total);
                int cantidadVentas = ventas.Count;
                Console.WriteLine($"- Total de ventas de los últimos 30 días: {totalVentas} pesos");
                Console.WriteLine($"- Cantidad de ventas de los últimos 30 días: {cantidadVentas}");

                // El día y hora en que se realizó la venta con el monto más alto
                var ventaMasAlta = ventas.OrderByDescending(v => v.Total).FirstOrDefault();
                if (ventaMasAlta != null)
                {
                    Console.WriteLine($"La venta más alta se realizó el {ventaMasAlta.Fecha} con un monto de {ventaMasAlta.Total}");
                }

                // Producto con mayor monto total de ventas
                var productoMasVendido = ventas
                    .SelectMany(v => v.VentaDetalle)
                    .GroupBy(d => d.ID_Producto)
                    .Select(g => new
                    {
                        ID_Producto = g.Key,
                        TotalVentas = g.Sum(d => d.TotalLinea)
                    })
                    .OrderByDescending(p => p.TotalVentas)
                    .FirstOrDefault();

                if (productoMasVendido != null)
                {
                    var producto = dbContext.Producto.FirstOrDefault(p => p.ID_Producto == productoMasVendido.ID_Producto);
                    Console.WriteLine($"- Producto con mayor monto total de ventas: {producto?.Nombre} ({productoMasVendido.TotalVentas} pesos)");
                }

                // Local con mayor monto de ventas
                var localMasVentas = ventas
                    .GroupBy(v => v.Local)
                    .Select(g => new
                    {
                        Local = g.Key,
                        TotalVentas = g.Sum(v => v.Total)
                    })
                    .OrderByDescending(l => l.TotalVentas)
                    .FirstOrDefault();

                if (localMasVentas != null)
                {
                    
                    Console.WriteLine($"El local con mayor monto de ventas es {localMasVentas.Local.Nombre} con un total de {localMasVentas.TotalVentas}");
                }

                // Marca con mayor margen de ganancias
                var marcaMaxMargen = dbContext.Marca
                    .OrderByDescending(m => m.Producto.Sum(p => p.VentaDetalle.Sum(d => (d.Precio_Unitario - d.Producto.Costo_Unitario) * d.Cantidad)))
                    .FirstOrDefault();

                if (marcaMaxMargen != null)
                {
                    Console.WriteLine($"La marca con mayor margen de ganancias es {marcaMaxMargen.Nombre}");
                }

                // Producto más vendido en cada local
                var productosMasVendidosPorLocal = dbContext.VentaDetalle
                    .GroupBy(d => new { d.Venta.ID_Local, d.ID_Producto })
                    .Select(g => new
                    {
                        ID_Local = g.Key.ID_Local,
                        ID_Producto = g.Key.ID_Producto,
                        TotalVentas = g.Sum(d => d.Cantidad)
                    })
                    .GroupBy(g => g.ID_Local)
                    .ToList();

                Console.WriteLine("- Productos más vendidos por local:");
                foreach (var localGroup in productosMasVendidosPorLocal)
                {
                    var local = dbContext.Local.FirstOrDefault(l => l.ID_Local == localGroup.Key);
                    Console.WriteLine($"Local: {local?.Nombre}");

                    var productoMsVendido = localGroup.OrderByDescending(p => p.TotalVentas).FirstOrDefault();
                    if (productoMsVendido != null)
                    {
                        var producto = dbContext.Producto.FirstOrDefault(p => p.ID_Producto == productoMsVendido.ID_Producto);
                        Console.WriteLine($"- Producto: {producto?.Nombre} ({productoMsVendido.TotalVentas} unidades)");
                    }

                    Console.WriteLine();
                }

                    Console.ReadLine();
            }
        }
    }
}
 
