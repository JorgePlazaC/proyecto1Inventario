using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Proyecto1Inventario.Pages;

public class IndexModel : PageModel
{
    public List<ProductosVendidosPorSucursal> ProductosVendidos { get; set; }
    public List<object[]> DatosGrafico { get; set; }
    public List<object[]> VentasTotales { get; set; }
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        InventarioContext context = new InventarioContext();
        ProductosVendidos = context.Detalles
                .GroupBy(d => d.Venta.SucursalId)
                .Select(g => new ProductosVendidosPorSucursal
                {
                    SucursalId = g.Key,
                    NombreSucursal = g.First().Venta.Sucursal.Nombre,
                    CantidadVendida = g.Sum(d => d.Cantidad)
                })
                .ToList();
        DatosGrafico = ProductosVendidos.Select(p => new object[] { p.NombreSucursal, p.CantidadVendida }).ToList();

        // Obtener el año actual
        int year = DateTime.Now.Year;

        // Realizar la consulta para obtener la suma de todas las ventas de todas las sucursales durante el presente año
        VentasTotales = context.Venta
            .Where(v => v.Fecha.Year == year)
            .GroupBy(v => v.Fecha.Month)
            .Select(g => new object[] { new DateTime(year, g.Key, 1).ToString("MMM"), g.Sum(v => v.Detalles.Sum(d => d.Precio*d.Cantidad)) })
            .ToList();
    }

    public class ProductosVendidosPorSucursal
    {
        public int SucursalId { get; set; }
        public string NombreSucursal { get; set; }
        public int CantidadVendida { get; set; }
    }
}
