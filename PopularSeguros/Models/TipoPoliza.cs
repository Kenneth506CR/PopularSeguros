using System;
using System.Collections.Generic;

namespace PopularSeguros.Models;

public partial class TipoPoliza
{
    public int IdTipoPoliza { get; set; }

    public string TipoPoliza1 { get; set; } = null!;

    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
