namespace Email_Planner.Client.Services.UserService
{
    public interface IUserService
    {
        event Action OnChange;
        List<User> Users { get; set; }
        Task GetUsers();
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        User CreateNewUser();
    }
}
