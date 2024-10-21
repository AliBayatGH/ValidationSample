using MealsyNotification.Models;
using MealsyNotification.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealsyNotification.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<NotificationModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNotifications([FromQuery] List<NotificationType?> notificationType, [FromQuery] string resourceKey)
    {
        var notifications = await _notificationService.GetPendingNotifications(notificationType, resourceKey);

        return Ok(notifications);
    }
}
