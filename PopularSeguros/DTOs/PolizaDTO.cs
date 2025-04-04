namespace PopularSeguros.DTOs
{
    public class PolizaDTO
    {
        public string? NumeroPoliza { get; set; } 
        public string TipoPoliza { get; set; } = "Desconocida";
        public string? CedulaAsegurado { get; set; }
        public decimal MontoAsegurado { get; set; }
        public DateOnly FechaVencimiento { get; set; }
        public DateOnly FechaEmision { get; set; }
        public string Cobertura { get; set; } = "Desconocida";
        public string EstadoPoliza { get; set; } = "Desconocida";
        public decimal Prima { get; set; }
        public DateOnly Periodo { get; set; }
        public DateOnly FechaInclusion { get; set; }
        public string Aseguradora { get; set; } = "Desconocida";
    }
}
