using Microsoft.AspNetCore.Mvc;
using SubManagement;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly SubscriptionService _subscriptionService;

    public SubscriptionController(SubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    // Получить все подписки
    [HttpGet]
    public IEnumerable<Subscription> Get()
    {
        return _subscriptionService.GetAllSubscriptions();
    }

    // Получить подписку по ID
    [HttpGet("{id}")]
    public ActionResult<Subscription> GetById(int id)
    {
        var subscription = _subscriptionService.GetSubscriptionById(id);
        if (subscription == null)
        {
            return NotFound("Подписка не найдена");
        }
        return subscription;
    }

    // Добавить новую подписку
    [HttpPost]
    public IActionResult Post([FromBody] Subscription subscription)
    {
        _subscriptionService.AddSubscription(subscription);
        return CreatedAtAction(nameof(GetById), new { id = subscription.Id }, subscription);
    }

    // Обновить подписку
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Subscription updatedSubscription)
    {
        _subscriptionService.UpdateSubscription(id, updatedSubscription);
        return NoContent();
    }

    // Удалить подписку
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _subscriptionService.DeleteSubscription(id);
        return NoContent();
    }

    // Получить общую стоимость всех активных подписок
    [HttpGet("Общая стоимость")]
    public decimal GetTotalMonthlyCost()
    {
        return _subscriptionService.GetTotalMonthlyCost();
    }

    // Получить предстоящие платежи
    [HttpGet("Предстоящие платежи")]
    public IEnumerable<Subscription> GetUpcomingPayments()
    {
        return _subscriptionService.GetUpcomingPayments();
    }

    // Получить общие расходы за период
    [HttpGet("Расходы за период")]
    public decimal GetTotalCostForPeriod([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        return _subscriptionService.GetTotalCostForPeriod(startDate, endDate);
    }
}