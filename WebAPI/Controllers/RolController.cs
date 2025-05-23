using Domain.DTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.IServices;
using WebAPI.Services.Services;

namespace WebAPI.Controllers
{
    public class RolController : Controller
    {
        private readonly IRolServices _rolServices;
        //Instancia la clase IArticleServices en todo el proyecto para usar todos lo metodos creados
        public RolController(IRolServices rolServices)
        {
            _rolServices = rolServices;
        }

        // Obtener todos los usuarios
        [HttpGet]
        public async Task<IActionResult> GetRols()
        {
            var rols = await _rolServices.GetRols();
            return Ok(rols);
        }

        // Obtener un usuario por ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var rol = await _rolServices.GetByIdRol(id);
            if (rol == null)
                return NotFound($"No se encontró un usuario con ID {id}.");

            return Ok(rol);
        }

        // Crear un nuevo usuario
        [HttpPost("crear")]
        public async Task<IActionResult> PostUser([FromBody] Rol request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = await _rolServices.CreateRol(request);
            return CreatedAtAction(nameof(GetRols), new { id = createdUser.PKRol }, createdUser);
        }

        // Actualizar un usuario existente
        [HttpPut("editar/{id:int}")]
        public async Task<IActionResult> PutRol(int id, [FromBody] Rol request)
        {
            if (id != request.PKRol)
                return BadRequest("El ID en la URL no coincide con el ID del cuerpo.");

            var updatedUser = await _rolServices.EditRol(request);
            if (updatedUser == null)
                return NotFound($"No se pudo actualizar el rol con ID {id}.");

            return Ok(updatedUser);
        }

        // Eliminar un rol
        [HttpDelete("eliminar/{id:int}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            var deleted = await _rolServices.DeleteRol(id);
            if (!deleted)
                return NotFound($"No se encontró el rol con ID {id} para eliminar.");

            return Ok(new { message = $"Rol con ID {id} eliminado correctamente." });
        }
    }
}
