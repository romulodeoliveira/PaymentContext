using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsGreaterOrEqualsThan(FirstName, 3, "Name.FirstName", "O primeiro nome deve conter pelo menos 3 caracteres")
            .IsGreaterOrEqualsThan(LastName, 3, "Name.LastName", "O sobrenome deve conter pelo menos 3 caracteres")
            .IsLowerOrEqualsThan(FirstName, 30, "Name.FirstName", "O primeiro nome deve conter no máximo 30 caracteres")
            .IsLowerOrEqualsThan(LastName, 30, "Name.LastName", "O sobrenome deve conter no máximo 30 caracteres")
        );
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
}