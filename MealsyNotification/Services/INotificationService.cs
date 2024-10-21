using MealsyNotification.Models;

namespace MealsyNotification.Services;

public interface INotificationService
{
    Task<List<NotificationModel>> GetPendingNotifications(List<NotificationType?> notificationType, string resourceKey);
}
