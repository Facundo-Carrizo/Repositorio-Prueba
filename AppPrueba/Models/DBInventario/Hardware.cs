using System;
using System.Collections.Generic;

namespace AppPrueba.Models.DBInventario;

public partial class Hardware
{
    public int IdHardware { get; set; }


    public string CodigoInventario { get; set; } = null!;

    public int IdModelo { get; set; }

    public string? NroSerie { get; set; }


    public string? Descripcion { get; set; }

    public string Estado { get; set; } = null!;

    public DateOnly FechaAlta { get; set; }

    public DateOnly? FechaBaja { get; set; }

    public virtual Modelo IdModeloNavigation { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual ICollection<Reparacione> Reparaciones { get; set; } = new List<Reparacione>();
}
