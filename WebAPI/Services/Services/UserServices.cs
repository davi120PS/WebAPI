using Domain.DTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Services.IServices;

namespace WebAPI.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly ApplicationDbContext _context;

        public UserServices(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtiene la lista de todos los usuarios, incluyendo sus roles.
        public async Task<List<User>> GetUsers()
        {
            try
            {
                // Obtiene la lista de todos los usuarios e incluye los datos de sus roles
                return await _context.Users.Include(x => x.Roles).ToListAsync();
            }
            catch (Exception ex)
            {
                // En caso de error, lanza una excepción personalizada
                throw new Exception("Surgió un error: " + ex.Message);
            }

        }

        // Obtiene un usuario por su ID, incluyendo su rol.
        public async Task<User> GetByIdUser(int id)
        {
            try
            {
                // Busca el usuario por su ID e incluye la información de su rol
                return await _context.Users
                    .Include(x => x.Roles)
                    .FirstOrDefaultAsync(x => x.PKUser == id);
            }
            catch (Exception ex)
            {
                // En caso de error, lanza una excepción personalizada
                throw new Exception("Surgió un error: " + ex.Message);
            }

        }

        // Crea un nuevo usuario con los datos proporcionados.
        public async Task<User> CreateUser(UserRequest i)
        {
            try
            {
                // Crea una nueva instancia del usuario con los datos recibidos del DTO
                User user = new User
                {
                    Name = i.Name,
                    Username = i.Username,
                    Password = i.Password,
                    FKRol = i.FKRol
                };

                // Agrega el nuevo usuario al contexto de la base de datos de forma asincrónica
                await _context.Users.AddAsync(user);

                // Guarda los cambios en la base de datos
                await _context.SaveChangesAsync();

                // Devuelve el usuario creado
                return user;
            }
            catch (Exception ex)
            {
                // En caso de error, lanza una excepción personalizada
                throw new Exception("Surgió un error: " + ex.Message);
            }

        }

        // Edita un usuario existente con los datos proporcionados.
        public async Task<User> EditUser(UserRequest i)
        {
            try
            {
                // Busca al usuario existente en la base de datos por su clave primaria
                var user = await _context.Users.FindAsync(i.PKUser);

                // Si no se encuentra el usuario, lanza una excepción personalizada
                if (user == null)
                    throw new Exception("Usuario no encontrado.");

                // Actualiza los campos del usuario con la información recibida
                user.Name = i.Name;
                user.Username = i.Username;
                user.Password = i.Password;
                user.FKRol = i.FKRol;

                // Marca la entidad como modificada para que EF Core actualice los datos
                _context.Entry(user).State = EntityState.Modified;

                // Guarda los cambios en la base de datos de forma asincrónica
                await _context.SaveChangesAsync();

                // Devuelve el usuario actualizado
                return user;
            }
            catch (Exception ex)
            {
                // Si ocurre un error, lanza una nueva excepción con un mensaje detallado
                throw new Exception("Surgió un error: " + ex.Message);
            }

        }

        // Elimina un usuario por su ID.
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                // Busca al usuario en la base de datos por su ID
                var user = await _context.Users.FindAsync(id);
                // Si no se encuentra el usuario, retorna false
                if (user == null)
                    return false;

                // Elimina el usuario encontrado del contexto
                _context.Users.Remove(user);
                // Guarda los cambios realizados en la base de datos de forma asincrónica
                await _context.SaveChangesAsync();
                // Retorna true indicando que la eliminación fue exitosa
                return true;
            }
            catch (Exception ex)
            {
                // Lanza una nueva excepción si ocurre un error durante el proceso
                throw new Exception("Surgió un error: " + ex.Message);
            }

        }

        // Método para autenticar un usuario con su nombre de usuario y contraseña.
        public async Task<User> Authenticate(string username, string password)
        {
            return await _context.Users.Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
        public async Task<User> Login(string username, string password)
        {
            return await _context.Users.Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

    }
}
