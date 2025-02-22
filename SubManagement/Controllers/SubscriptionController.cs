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

    // �������� ��� ��������
    [HttpGet]
    public IEnumerable<Subscription> Get()
    {
        return _subscriptionService.GetAllSubscriptions();
    }

    // �������� �������� �� ID
    [HttpGet("{id}")]
    public ActionResult<Subscription> GetById(int id)
    {
        var subscription = _subscriptionService.GetSubscriptionById(id);
        if (subscription == null)
        {
            return NotFound("�������� �� �������");
        }
        return subscription;
    }

    // �������� ����� ��������
    [HttpPost]
    public IActionResult Post([FromBody] Subscription subscription)
    {
        _subscriptionService.AddSubscription(subscription);
        return CreatedAtAction(nameof(GetById), new { id = subscription.Id }, subscription);
    }

    // �������� ��������
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Subscription updatedSubscription)
    {
        _subscriptionService.UpdateSubscription(id, updatedSubscription);
        return NoContent();
    }

    // ������� ��������
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _subscriptionService.DeleteSubscription(id);
        return NoContent();
    }

    // �������� ����� ��������� ���� �������� ��������
    [HttpGet("����� ���������")]
    public decimal GetTotalMonthlyCost()
    {
        return _subscriptionService.GetTotalMonthlyCost();
    }

    // �������� ����������� �������
    [HttpGet("����������� �������")]
    public IEnumerable<Subscription> GetUpcomingPayments()
    {
        return _subscriptionService.GetUpcomingPayments();
    }

    // �������� ����� ������� �� ������
    [HttpGet("������� �� ������")]
    public decimal GetTotalCostForPeriod([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        return _subscriptionService.GetTotalCostForPeriod(startDate, endDate);
    }
}