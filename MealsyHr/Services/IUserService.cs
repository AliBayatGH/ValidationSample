using MealsyHr.Models;

namespace MealsyHr.Services;

public interface IUserService
{
    void AddUser(User user);
    List<User> GetAllUsers();
    void SendUserToMev(User user);
}
