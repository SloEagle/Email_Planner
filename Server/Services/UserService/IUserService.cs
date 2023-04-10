using Email_Planner.Shared;

namespace Email_Planner.Server.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<User>>> GetUsers();
        Task<ServiceResponse<User>> GetUserById(int id);
        Task<ServiceResponse<List<User>>> UpdateUser(User user);
        Task<ServiceResponse<List<User>>> AddUser(User user);
        Task<ServiceResponse<List<User>>> DeleteUser(int id);
    }
}
