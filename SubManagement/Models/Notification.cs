namespace SubManagement
{
    public class Notification
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public string Message { get; set; }
        public DateTime NotificationDate { get; set; }
    }
}
