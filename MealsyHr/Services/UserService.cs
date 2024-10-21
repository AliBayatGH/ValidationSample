using MealsyHr.Models;

namespace MealsyHr.Services;

public class UserService : IUserService
{
    private static List<User> _users = new List<User>();
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public void AddUser(User user)
    {
        user.Id = _users.Count + 1; // Simple Id assignment
        user.Status = (short)EntityStatus.Pending;
        _users.Add(user);
        SendUserToMev(user); // Send user to MevService
    }

    public List<User> GetAllUsers()
    {
        return _users;
    }

    public async void SendUserToMev(User user)
    {
        await _httpClient.PostAsJsonAsync("http://localhost:5000/api/users/save", user);
    }
}
