using System;
using System.Collections.Generic;

namespace AppPrueba.Models.DBInventario;

public partial class Movimiento
{
    public int IdMovimiento { get; set; }

    public int IdHardware { get; set; }

    public string TipoMovimiento { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public int? IdUbicacion { get; set; }

    public int? IdResponsable { get; set; }

    public string? Observaciones { get; set; }

    public virtual Hardware IdHardwareNavigation { get; set; } = null!;

    public virtual Responsable? IdResponsableNavigation { get; set; }

    public virtual Ubicacione? IdUbicacionNavigation { get; set; }
}
