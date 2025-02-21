using System.ComponentModel.DataAnnotations;


namespace L01_22MM652_2022AR652.Models
{
    public class Comentario
    {
        public int ComentarioId { get; set; }
        public int PublicacionId { get; set; }
        public string Texto { get; set; }
        public int UsuarioId { get; set; }
    }
}
