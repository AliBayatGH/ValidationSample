namespace POSClientApplet.Models;

public class NotificationModel
{
    public NotificationType NotificationType { get; set; }
    public Guid BusinessLocationId { get; set; }
    public string ResourceKey { get; set; }
    public List<object> Notifications { get; set; }
}
