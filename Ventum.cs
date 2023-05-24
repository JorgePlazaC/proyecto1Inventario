using System;
using System.Collections.Generic;

namespace Proyecto1Inventario;

public partial class Ventum
{
    public int Id { get; set; }

    public int Correlativo { get; set; }

    public DateTime Fecha { get; set; }

    public TimeSpan Hora { get; set; }

    public string RutCliente { get; set; } = null!;

    public int SucursalId { get; set; }

    public virtual ICollection<Detalle> Detalles { get; set; } = new List<Detalle>();

    public virtual Sucursal Sucursal { get; set; } = null!;
}
