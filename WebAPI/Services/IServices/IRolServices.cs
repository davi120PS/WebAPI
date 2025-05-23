using Domain.Entities;

namespace WebAPI.Services.IServices
{
    public interface IRolServices
    {
        Task<List<Rol>> GetRols();
        Task<Rol> GetByIdRol(int id);
        Task<Rol> CreateRol(Rol i);
        Task<Rol> EditRol(Rol i);
        Task<bool> DeleteRol(int id);
    }
}
