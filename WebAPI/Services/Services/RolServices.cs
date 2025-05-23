using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Services.IServices;

namespace WebAPI.Services.Services
{
    public class RolServices : IRolServices
    {
        private readonly ApplicationDbContext _context;
        public RolServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Rol>> GetRols()
        {
            try
            {
                return await _context.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Surgió un error" + ex.Message);
            }
        }
        public async Task<Rol> GetByIdRol(int id)
        {
            try
            {
                Rol response = await _context.Roles
                    .FirstOrDefaultAsync(x => x.PKRol == id);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Surgió un error" + ex.Message);
            }
        }
        public async Task<Rol> CreateRol(Rol i)
        {
            try
            {
                Rol request = new Rol()
                {
                    Name = i.Name
                };
                //Con esto se manda a la bd de forma asincrona
                var result = await _context.Roles.AddAsync(request);
                _context.SaveChanges();

                return request;
            }
            catch (Exception ex)
            {
                throw new Exception("Surgió un error" + ex.Message);
            }
        }
        public async Task<Rol> EditRol(Rol i)
        {
            try
            {
                Rol request = await _context.Roles.FindAsync(i.PKRol);
                request.Name = i.Name;
                //Con esto se manda a la bd de forma asincrona

                _context.Entry(request).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return request;

            }
            catch (Exception ex)
            {
                throw new Exception("Surgió un error" + ex.Message);
            }
        }
        public async Task<bool> DeleteRol(int id)
        {
            try
            {
                var rol = await _context.Roles.FindAsync(id);
                if (rol == null)
                    return false;

                _context.Roles.Remove(rol);
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
