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

        public async Task<List<User>> GetUsers()
        {
            try
            {
                return await _context.Users.Include(x => x.Roles).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Surgió un error: " + ex.Message);
            }
        }

        public async Task<User> GetByIdUser(int id)
        {
            try
            {
                return await _context.Users
                    .Include(x => x.Roles)
                    .FirstOrDefaultAsync(x => x.PKUser == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Surgió un error: " + ex.Message);
            }
        }

        public async Task<User> CreateUser(UserRequest i)
        {
            try
            {
                User user = new User
                {
                    Name = i.Name,
                    Username = i.Username,
                    Password = i.Password,
                    FKRol = i.FKRol
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Surgió un error: " + ex.Message);
            }
        }

        public async Task<User> EditUser(UserRequest i)
        {
            try
            {
                var user = await _context.Users.FindAsync(i.PKUser);
                if (user == null)
                    throw new Exception("Usuario no encontrado.");

                user.Name = i.Name;
                user.Username = i.Username;
                user.Password = i.Password;
                user.FKRol = i.FKRol;

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Surgió un error: " + ex.Message);
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return false;

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Surgió un error: " + ex.Message);
            }
        }
    }
}
