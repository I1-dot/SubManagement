namespace SubManagement
{
    public class Subscription
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public decimal MonthlyCost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public bool IsActive { get; set; }
    }
}
