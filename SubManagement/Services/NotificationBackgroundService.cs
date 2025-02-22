using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using SubManagement;

public class NotificationBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public NotificationBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var subscriptionService = scope.ServiceProvider.GetRequiredService<SubscriptionService>();
                var notificationService = scope.ServiceProvider.GetRequiredService<NotificationService>();

                // Проверяем предстоящие платежи каждые 24 часа
                var upcomingSubscriptions = subscriptionService.GetUpcomingPayments();

                foreach (var subscription in upcomingSubscriptions)
                {
                    // Проверяем, есть ли уже уведомление для этой подписки
                    var existingNotification = notificationService.GetNotificationsForSubscription(subscription.Id)
                        .FirstOrDefault(n => n.Message.Contains("Оплата"));

                    if (existingNotification == null)
                    {
                        // Создаем новое уведомление
                        var notification = new Notification
                        {
                            SubscriptionId = subscription.Id,
                            Message = $"Оплата за {subscription.ServiceName} должна быть произведена {subscription.NextPaymentDate.ToShortDateString()}",
                            NotificationDate = DateTime.Now
                        };

                        notificationService.AddNotification(notification);
                    }
                }
            }

            // Ожидаем 24 часа перед следующей проверкой
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}