using System;
using System.Collections.Generic;

namespace PopularSeguros.Models;

public partial class Cliente
{
    public string CedulaAsegurado { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public string? TipoPersona { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    public virtual ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();
}
