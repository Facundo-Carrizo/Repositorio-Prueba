using System;
using System.Collections.Generic;

namespace AppPrueba.Models.DBInventario;

public partial class Modelo
{
    public int IdModelo { get; set; }

    public int IdMarca { get; set; }

    public int IdTipo { get; set; }

    public string NombreModelo { get; set; } = null!;

    public virtual ICollection<Hardware> Hardwares { get; set; } = new List<Hardware>();

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual TipoHardware IdTipoNavigation { get; set; } = null!;
}
