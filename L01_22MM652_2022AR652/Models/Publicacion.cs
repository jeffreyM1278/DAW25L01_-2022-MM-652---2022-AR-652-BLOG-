using System.ComponentModel.DataAnnotations;


namespace L01_22MM652_2022AR652.Models
{
    public class Publicacion
    {
        public int PublicacionId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
    }
}
