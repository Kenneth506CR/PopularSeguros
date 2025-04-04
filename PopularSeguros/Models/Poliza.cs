using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PopularSeguros.Models;

public partial class Poliza
{
    public string? NumeroPoliza { get; set; }

    public int? IdTipoPoliza { get; set; }

    public string? CedulaAsegurado { get; set; }

    public decimal MontoAsegurado { get; set; } 

    public DateOnly FechaVencimiento { get; set; }

    public DateOnly FechaEmision { get; set; }

    public decimal Prima { get; set; }

    public DateOnly Periodo { get; set; }

    public DateOnly FechaInclusion { get; set; }

    public int IdAseguradora { get; set; }

    public int IdCobertura { get; set; }

    public int? IdEstadoPoliza { get; set; }

    [JsonIgnore]
    public virtual Cliente CedulaAseguradoNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Aseguradora IdAseguradoraNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Cobertura IdCoberturaNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual EstadoPoliza IdEstadoPolizaNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual TipoPoliza IdTipoPolizaNavigation { get; set; } = null!;

}
