using System;
using System.Collections.Generic;

namespace AppPrueba.Models.DBInventario;

public partial class TipoHardware
{
    public int IdTipo { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();
}
