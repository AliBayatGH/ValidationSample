using POSClientApplet.Models;
using System.Text.Json;

namespace POSClientApplet.Services;

public class UserValidationService : IUserValidationService
{
    private readonly HttpClient _httpClient;
    private readonly string _mevServiceBaseUrl = "http://localhost:5000/";
    private readonly string _mealsyNotificationBaseUrl = "http://localhost:5002/";

    public UserValidationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task ValidatePendingUsers()
    {
        // Send request to MealsyNotification to get pending users
        var notifications = await _httpClient.GetFromJsonAsync<NotificationModel[]>($"{_mealsyNotificationBaseUrl}api/notifications?notificationType=MevUser&resourceKey=someKey");

        foreach (var notification in notifications)
        {
            foreach (var userObj in notification.Notifications)
            {
                var userJson = JsonSerializer.Serialize(userObj);
                var user = JsonSerializer.Deserialize<User>(userJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (user != null)
                {
                    var isValid = ValidateExternally(user);

                    // Update user status based on validation (1 = Active, -1 = Failed)
                    short newStatus = isValid ? (short)EntityStatus.Active : (short)EntityStatus.Failed;
                    await _httpClient.PostAsJsonAsync($"{_mevServiceBaseUrl}api/users/update/{user.Id}", newStatus);
                }
            }
        }
    }

    private bool ValidateExternally(User user)
    {
        // Web-Srm validation logic simulation
        return user.Name.Length % 2 == 0;
    }
}
