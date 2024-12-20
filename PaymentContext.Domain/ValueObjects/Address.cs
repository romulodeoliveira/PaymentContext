using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Address : ValueObject
{
    public Address(
        string street, 
        string number, 
        string neighborhood, 
        string city, string state, 
        string country, 
        string zipCode)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;

        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsGreaterOrEqualsThan(Street, 1, "Address.Street", "A rua deve conter pelo menos 1 caractere")
            .IsLowerOrEqualsThan(Street, 20, "Address.Street", "A rua deve conter no máximo 20 caracteres")
            .IsGreaterOrEqualsThan(Number, 1, "Address.Number", "O número deve conter pelo menos 1 caractere")
            .IsLowerOrEqualsThan(Number, 20, "Address.Number", "O número deve conter no máximo 20 caracteres")
            .IsGreaterOrEqualsThan(Neighborhood, 1, "Address.Neighborhood", "O bairro deve conter pelo menos 1 caractere")
            .IsLowerOrEqualsThan(Neighborhood, 20, "Address.Neighborhood", "O bairro deve conter no máximo 20 caracteres")
            .IsGreaterOrEqualsThan(City, 1, "Address.City", "A cidade deve conter pelo menos 1 caractere")
            .IsLowerOrEqualsThan(City, 20, "Address.City", "A cidade deve conter no máximo 20 caracteres")
            .IsGreaterOrEqualsThan(State, 1, "Address.State", "O estado deve conter pelo menos 1 caractere")
            .IsLowerOrEqualsThan(State, 20, "Address.State", "O estado deve conter no máximo 20 caracteres")
            .IsGreaterOrEqualsThan(Country, 1, "Address.Country", "O país deve conter pelo menos 1 caractere")
            .IsLowerOrEqualsThan(Country, 20, "Address.Country", "O país deve conter no máximo 20 caracteres")
            .IsGreaterOrEqualsThan(ZipCode, 1, "Address.ZipCode", "O código postal deve conter pelo menos 1 caractere")
            .IsLowerOrEqualsThan(ZipCode, 20, "Address.ZipCode", "O código postal deve conter no máximo 20 caracteres")
        );
    }

    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
}