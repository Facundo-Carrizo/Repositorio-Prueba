using System;
using System.Collections.Generic;

namespace AppPrueba.Models.DBInventario;

public partial class Consumible
{
    public int IdConsumible { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int StockActual { get; set; }

    public int StockMinimo { get; set; }

    public virtual ICollection<ReparacionConsumible> ReparacionConsumibles { get; set; } = new List<ReparacionConsumible>();
}
