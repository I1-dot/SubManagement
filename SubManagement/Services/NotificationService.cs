using SubManagement;

public class NotificationService
{
    private static List<Notification> _notifications = new List<Notification>
    {
        new Notification { Id = 1, SubscriptionId = 1, Message = "Оплата за Яндекс Плюс должна быть произведена", NotificationDate = DateTime.Now.AddDays(7) }
    };

    // Получить все уведомления
    public IEnumerable<Notification> GetAllNotifications()
    {
        return _notifications;
    }

    // Добавить новое уведомление
    public void AddNotification(Notification notification)
    {
        notification.Id = _notifications.Max(n => n.Id) + 1;
        _notifications.Add(notification);
    }

    // Получить уведомления для конкретной подписки
    public IEnumerable<Notification> GetNotificationsForSubscription(int subscriptionId)
    {
        return _notifications.Where(n => n.SubscriptionId == subscriptionId);
    }
}