using System;
using System.Collections.Generic;

namespace AppPrueba.Models.DBInventario;

public partial class ReparacionConsumible
{
    public int IdReparacionConsumible { get; set; }

    public int IdReparacion { get; set; }

    public int IdConsumible { get; set; }

    public int Cantidad { get; set; }

    public virtual Consumible IdConsumibleNavigation { get; set; } = null!;

    public virtual Reparacione IdReparacionNavigation { get; set; } = null!;
}
