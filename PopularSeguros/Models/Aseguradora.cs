using System;
using System.Collections.Generic;

namespace PopularSeguros.Models;

public partial class Aseguradora
{
    public int IdAseguradora { get; set; }

    public string Aseguradora1 { get; set; } = null!;

    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
