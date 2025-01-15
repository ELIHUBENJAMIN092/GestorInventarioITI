using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos.Server.Models;

namespace Productos.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ProductosContext _context;

        public RolesController(ProductosContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CrearRol(Rol rol)
        {
            await _context.Roles.AddAsync(rol);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("lista")]
        public async Task<ActionResult<IEnumerable<Rol>>> ListaRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpGet]
        [Route("verRol")]
        public async Task<IActionResult> GetRol(int id)
        {
            // Obtener el rol de la base de datos
            Rol rol = await _context.Roles.FindAsync(id);

            // Devolver el rol
            if (rol == null)
            {
                return NotFound();
            }

            return Ok(rol);
        }

        [HttpPut]
        [Route("editarRol")]
        public async Task<IActionResult> UpdateRol(int id, Rol rol)
        {
            // Actualizar el rol en la base de datos
            var rolExistente = await _context.Roles.FindAsync(id);
            if (rolExistente == null)
            {
                return NotFound();
            }

            rolExistente.Nombre = rol.Nombre;
            rolExistente.Descripcion = rol.Descripcion;

            await _context.SaveChangesAsync();

            // Devolver un mensaje de éxito
            return Ok();
        }

        [HttpDelete]
        [Route("eliminarRol")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            // Eliminar rol de la base de datos
            var rolBorrado = await _context.Roles.FindAsync(id);
            if (rolBorrado == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(rolBorrado);
            await _context.SaveChangesAsync();

            // Devolver un mensaje de éxito
            return Ok();
        }
    }
}