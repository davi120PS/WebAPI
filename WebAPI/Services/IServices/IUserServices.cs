using Domain.DTO;
using Domain.Entities;

namespace WebAPI.Services.IServices
{
    public interface IUserServices
    {
        Task<List<User>> GetUsers();
        Task<User> GetByIdUser(int id);
        Task<User> CreateUser(UserRequest i);
        Task<User> EditUser(UserRequest i);
        Task<bool> DeleteUser(int id);
    }
}
