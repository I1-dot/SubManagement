using SubManagement;

public class SubscriptionService
{
    private static List<Subscription> _subscriptions = new List<Subscription>
    {
        new Subscription { Id = 1, ServiceName = "Яндекс Плюс", MonthlyCost = 299m, StartDate = DateTime.Now, NextPaymentDate = DateTime.Now.AddMonths(1), IsActive = true }
    };

    // Получить все подписки
    public IEnumerable<Subscription> GetAllSubscriptions()
    {
        return _subscriptions;
    }

    // Получить подписку по ID
    public Subscription GetSubscriptionById(int id)
    {
        return _subscriptions.FirstOrDefault(s => s.Id == id);
    }

    // Добавить новую подписку
    public void AddSubscription(Subscription subscription)
    {
        subscription.Id = _subscriptions.Max(s => s.Id) + 1;
        _subscriptions.Add(subscription);
    }

    // Обновить подписку
    public void UpdateSubscription(int id, Subscription updatedSubscription)
    {
        var subscription = _subscriptions.FirstOrDefault(s => s.Id == id);
        if (subscription != null)
        {
            subscription.ServiceName = updatedSubscription.ServiceName;
            subscription.MonthlyCost = updatedSubscription.MonthlyCost;
            subscription.StartDate = updatedSubscription.StartDate;
            subscription.NextPaymentDate = updatedSubscription.NextPaymentDate;
            subscription.IsActive = updatedSubscription.IsActive;
        }
    }

    // Удалить подписку
    public void DeleteSubscription(int id)
    {
        var subscription = _subscriptions.FirstOrDefault(s => s.Id == id);
        if (subscription != null)
        {
            _subscriptions.Remove(subscription);
        }
    }

    // Получить общую стоимость всех активных подписок
    public decimal GetTotalMonthlyCost()
    {
        return _subscriptions.Where(s => s.IsActive).Sum(s => s.MonthlyCost);
    }

    // Получить предстоящие платежи (подписки, у которых NextPaymentDate в ближайшие 7 дней)
    public IEnumerable<Subscription> GetUpcomingPayments()
    {
        return _subscriptions.Where(s => s.NextPaymentDate <= DateTime.Now.AddDays(7) && s.IsActive);
    }

    // Получить общие расходы за период
    public decimal GetTotalCostForPeriod(DateTime startDate, DateTime endDate)
    {
        return _subscriptions
            .Where(s => s.IsActive && s.StartDate >= startDate && s.NextPaymentDate <= endDate)
            .Sum(s => s.MonthlyCost);
    }
}