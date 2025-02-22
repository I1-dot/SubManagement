using Microsoft.AspNetCore.Mvc;
using SubManagement;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly NotificationService _notificationService;

    public NotificationController(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    // Получить все уведомления
    [HttpGet]
    public IEnumerable<Notification> Get()
    {
        return _notificationService.GetAllNotifications();
    }

    // Добавить новое уведомление
    [HttpPost]
    public IActionResult Post([FromBody] Notification notification)
    {
        _notificationService.AddNotification(notification);
        return CreatedAtAction(nameof(Get), new { id = notification.Id }, notification);
    }

    // Получить уведомления для конкретной подписки
    [HttpGet("Подписка/{subscriptionId}")]
    public IEnumerable<Notification> GetNotificationsForSubscription(int subscriptionId)
    {
        return _notificationService.GetNotificationsForSubscription(subscriptionId);
    }
}