namespace PopularSeguros.DTOs
{
    public class ClienteDTO
    {
        public string CedulaAsegurado { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string PrimerApellido { get; set; } = null!;

        public string? SegundoApellido { get; set; }

        public string? TipoPersona { get; set; }

        public DateOnly FechaNacimiento { get; set; }
    }
}
