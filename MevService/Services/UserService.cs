using MevService.Models;

namespace MevService.Services;

public class UserService : IUserService
{
    private static List<User> _users = new List<User>();

    public void SaveUser(User user)
    {
        user.Id = _users.Count + 1;
        user.Status = 3; // Pending status
        _users.Add(user);
    }

    public List<User> GetPendingUsers()
    {
        return _users.Where(u => u.Status == 3).ToList(); // Return pending users
    }

    public User GetUser(int userId)
    {
        return _users.FirstOrDefault(u => u.Id == userId);
    }

    public void UpdateUserStatus(int userId, short status)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user != null)
        {
            user.Status = status;
        }
    }
}
