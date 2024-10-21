using MevService.Models;

namespace MevService.Services;

public interface IUserService
{
    void SaveUser(User user);
    List<User> GetPendingUsers();
    void UpdateUserStatus(int userId, short status);
    User GetUser(int userId);
}
