namespace PaymentContext.Domain.Entities;

public class Subscription
{
    private IList<Payment> _payments;
    public Subscription(DateTime? expiredDate)
    {
        CreatedDate = DateTime.Now;
        LastUpdatedDate = DateTime.Now;
        ExpiredDate = expiredDate;
        IsActive = true;
        Payments = new List<Payment>();
    }

    public DateTime CreatedDate { get; private set;}
    public DateTime LastUpdatedDate { get; private set;}
    public DateTime? ExpiredDate { get; private set;}
    public bool IsActive { get; private set;}
    public IReadOnlyCollection<Payment> Payments { get; private set; }

    public void AddPayment(Payment payment)
    {
        _payments.Add(payment);
    }

    public void Activate()
    {
        IsActive = true;
        LastUpdatedDate = DateTime.Now;
    }

    public void Inactivate()
    {
        IsActive = false;
        LastUpdatedDate = DateTime.Now;
    }
}