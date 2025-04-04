namespace PopularSeguros.DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string Usuario1 { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }
    }
}
