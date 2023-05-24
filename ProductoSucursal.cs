using System;
using System.Collections.Generic;

namespace Proyecto1Inventario;

public partial class ProductoSucursal
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public int SucursalId { get; set; }

    public int Stock { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Sucursal Sucursal { get; set; } = null!;
}
