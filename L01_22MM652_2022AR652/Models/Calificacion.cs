using System.ComponentModel.DataAnnotations;



namespace L01_22MM652_2022AR652.Models
{
    public class Calificacion
    {
        public int CalificacionId { get; set; }
        public int PublicacionId { get; set; }
        public int UsuarioId { get; set; }
        public int Valor { get; set; }

    }
}
