using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_22MM652_2022AR652.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_22MM652_2022AR652.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly blogDBContext _context;

        public UsuariosController(blogDBContext context)
        {
            _context = context;
        }

 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();
            return usuario;
        }

        
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.UsuarioId }, usuario);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
                return BadRequest();

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpGet("por-nombre-apellido/{nombre}/{apellido}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosPorNombreApellido(string nombre, string apellido)
        {
            return await _context.Usuarios
                .Where(u => u.Nombre.Contains(nombre) && u.Apellido.Contains(apellido))
                .ToListAsync();
        }

       
        [HttpGet("por-rol/{rolId}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosPorRol(int rolId)
        {
            return await _context.Usuarios
                .Where(u => u.RolId == rolId)
                .ToListAsync();
        }

        
        [HttpGet("top-usuarios-comentarios/{n}")]
        public async Task<ActionResult<IEnumerable<object>>> GetTopUsuariosPorComentarios(int n)
        {
            var topUsuarios = await _context.Comentarios
                .GroupBy(c => c.UsuarioId)
                .Select(g => new
                {
                    UsuarioId = g.Key,
                    CantidadComentarios = g.Count()
                })
                .OrderByDescending(u => u.CantidadComentarios)
                .Take(n)
                .ToListAsync();

            return Ok(topUsuarios);
        }
    }
}
