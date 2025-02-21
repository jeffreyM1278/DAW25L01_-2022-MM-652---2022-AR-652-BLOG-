using System.ComponentModel.DataAnnotations;


namespace L01_22MM652_2022AR652.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
