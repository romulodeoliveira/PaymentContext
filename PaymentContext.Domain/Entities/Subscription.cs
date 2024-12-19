namespace PaymentContext.Domain.Entities;

public class Subscription
{
    public DateTime CreatedDate { get; set;}
    public DateTime LastUpdatedDate { get; set;}
    public DateTime? ExpiredDate { get; set;}
    public bool IsActive { get; set;}
    public List<Payment> Payments { get; set; }
}