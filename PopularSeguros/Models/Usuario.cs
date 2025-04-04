using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PopularSeguros.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    [Column("usuario")]
    public string Usuario1 { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }
}
