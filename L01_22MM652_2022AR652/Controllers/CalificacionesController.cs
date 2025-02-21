using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_22MM652_2022AR652.Models;
using Microsoft.EntityFrameworkCore;


namespace L01_22MM652_2022AR652.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        private readonly blogDBContext _context;

        public CalificacionesController(blogDBContext context)
        {
            _context = context;
        }

        // 📌 Obtener todas las calificaciones registradas en la base de datos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calificacion>>> GetCalificaciones()
        {
            return await _context.Calificaciones.ToListAsync(); // Retorna todas las calificaciones en una lista
        }

        // 📌 Obtener una calificación por su ID
        [HttpGet("{id}")] // Ruta: api/calificaciones/{id}
        public async Task<ActionResult<Calificacion>> GetCalificacion(int id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id); // Busca la calificación por ID

            if (calificacion == null) // Si no existe, retorna un error 404
                return NotFound();

            return calificacion; // Si existe, la retorna
        }

        // 📌 Agregar una nueva calificación
        [HttpPost] // Ruta: api/calificaciones
        public async Task<ActionResult<Calificacion>> PostCalificacion(Calificacion calificacion)
        {
            _context.Calificaciones.Add(calificacion); // Agrega la nueva calificación al contexto
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

            // Retorna la calificación creada con su ID asignado
            return CreatedAtAction(nameof(GetCalificacion), new { id = calificacion.CalificacionId }, calificacion);
        }

        // 📌 Actualizar una calificación existente
        [HttpPut("{id}")] // Ruta: api/calificaciones/{id}
        public async Task<IActionResult> PutCalificacion(int id, Calificacion calificacion)
        {
            if (id != calificacion.CalificacionId) // Valida que el ID de la URL coincida con el del objeto
                return BadRequest(); // Retorna error 400 si no coinciden

            _context.Entry(calificacion).State = EntityState.Modified; // Marca la entidad como modificada
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

            return NoContent(); // Retorna respuesta 204 (sin contenido)
        }

        // 📌 Eliminar una calificación por su ID
        [HttpDelete("{id}")] // Ruta: api/calificaciones/{id}
        public async Task<IActionResult> DeleteCalificacion(int id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id); // Busca la calificación en la base de datos

            if (calificacion == null) // Si no existe, retorna error 404
                return NotFound();

            _context.Calificaciones.Remove(calificacion); // Elimina la calificación
            await _context.SaveChangesAsync(); // Guarda los cambios

            return NoContent(); // Retorna respuesta 204 (sin contenido)
        }

        // 📌 Obtener todas las calificaciones de una publicación específica
        [HttpGet("por-publicacion/{publicacionId}")] // Ruta: api/calificaciones/por-publicacion/{publicacionId}
        public async Task<ActionResult<IEnumerable<Calificacion>>> GetCalificacionesPorPublicacion(int publicacionId)
        {
            return await _context.Calificaciones
                .Where(c => c.PublicacionId == publicacionId) // Filtra por el ID de la publicación
                .ToListAsync(); // Retorna la lista de calificaciones
        }
    }

}
