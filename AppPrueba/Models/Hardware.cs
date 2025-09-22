using System;
using System.Collections.Generic;

namespace AppPrueba.Models
{
    public class Hardware
    {
        public int IdHardware { get; set; }
        public string CodigoInventario { get; set; }
        public Modelo Modelo { get; set; }
        public string NumeroSerie { get; set; }
        public string Estado { get; set; }
        public DateTime FechaAlta { get; set; }
    }

    public class Modelo
    {
        public int IdModelo { get; set; }
        public string NombreModelo { get; set; }
    }
}