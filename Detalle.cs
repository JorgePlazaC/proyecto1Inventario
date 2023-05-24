using System;
using System.Collections.Generic;

namespace Proyecto1Inventario;

public partial class Detalle
{
    public int ProductoId { get; set; }

    public int VentaId { get; set; }

    public int Id { get; set; }

    public int Precio { get; set; }

    public int Cantidad { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Ventum Venta { get; set; } = null!;
}
