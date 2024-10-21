using MealsyNotification.Models;

namespace MealsyNotification.Services;

public class NotificationService : INotificationService
{
    private readonly HttpClient _httpClient;

    public NotificationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<NotificationModel>> GetPendingNotifications(List<NotificationType?> notificationType, string resourceKey)
    {
        var response = await _httpClient.GetAsync("api/users/pending");

        if (!response.IsSuccessStatusCode)
        {
            return new List<NotificationModel>();
        }

        var pendingUsers = await response.Content.ReadFromJsonAsync<List<object>>();

        return new List<NotificationModel>
        {
            new NotificationModel
            {
                NotificationType = NotificationType.MevUser,
                BusinessLocationId = Guid.NewGuid(), // Mocked location Id
                ResourceKey = resourceKey,
                Notifications = pendingUsers
            }
        };
    }
}
