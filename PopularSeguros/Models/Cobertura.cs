using System;
using System.Collections.Generic;

namespace PopularSeguros.Models;

public partial class Cobertura
{
    public int IdCobertura { get; set; }

    public string Cobertura1 { get; set; } = null!;

    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
