using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Prueba.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly TodoContext context;

        public UsersController(TodoContext context)
        {
            this.context = context;
        }

        // Obtener todos los usuarios
        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await context.Users.ToListAsync();
        }

        // Obtener un usuario por ID
        [HttpGet("{id:int}", Name = "GetUserById")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        // Crear un nuevo usuario
        [HttpPost]
        public async Task<CreatedAtRouteResult> Post([FromBody] User user)
        {
            context.Add(user);
            await context.SaveChangesAsync();
            return CreatedAtRoute("GetUserById", new { id = user.UserId }, user);
        }

        // Actualizar un usuario existente
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] User user)
        {
            var existsUser = await context.Users.AnyAsync(x => x.UserId == id);

            if (!existsUser)
            {
                return NotFound();
            }

            user.UserId = id;
            context.Update(user);
            await context.SaveChangesAsync();
            return NoContent();
        }

        // Eliminar un usuario por ID
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if (user is null)
            {
                return NotFound();
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}

