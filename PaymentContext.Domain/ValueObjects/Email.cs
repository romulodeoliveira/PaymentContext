using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string address)
    {
        Address = address;

        if(string.IsNullOrEmpty(Address))
        {
            AddNotification("Email.Address", "Endereço de e-mail inválido");
        }
    }

    public string Address { get; private set; }
}