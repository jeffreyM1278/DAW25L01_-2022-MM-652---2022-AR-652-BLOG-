using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_22MM652_2022AR652.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_22MM652_2022AR652.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly blogDBContext _context;

        public ComentariosController(blogDBContext context)
        {
            _context = context;
        }

        // 📌 Obtener todos los comentarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentarios()
        {
            return await _context.Comentarios.ToListAsync();
        }

        // 📌 Obtener un comentario por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Comentario>> GetComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null)
                return NotFound();
            return comentario;
        }

        // 📌 Agregar un nuevo comentario
        [HttpPost]
        public async Task<ActionResult<Comentario>> PostComentario(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetComentario), new { id = comentario.ComentarioId }, comentario);
        }

        // 📌 Actualizar un comentario existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentario(int id, Comentario comentario)
        {
            if (id != comentario.ComentarioId)
                return BadRequest();

            _context.Entry(comentario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 📌 Eliminar un comentario por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null)
                return NotFound();

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 📌 Obtener comentarios filtrados por usuario
        [HttpGet("por-usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentariosPorUsuario(int usuarioId)
        {
            return await _context.Comentarios
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();
        }

    }
}
