using System;
using System.Collections.Generic;

namespace PopularSeguros.Models;

public partial class EstadoPoliza
{
    public int IdEstadoPoliza { get; set; }

    public string EstadoPoliza1 { get; set; } = null!;

    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
