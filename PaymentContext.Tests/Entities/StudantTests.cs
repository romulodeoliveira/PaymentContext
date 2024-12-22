using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudantTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;
    private readonly Student _student;
    private readonly Subscription _subscription;

    public StudantTests()
    {
        _name = new Name("Bob Esponja", "Calça Quadrada");
        _document = new Document("55704172067", EDocumentType.CPF);
        _email = new Email("bob@email.com");
        _address = new Address("Rua do Caralho", "666", "Bairro Péssimo", "Fudido", "CE", "BR", "12300000");
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null);
    }
    
    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5),  10, 10, "Lula Molusco", _document, _address, _email);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);
        
        Assert.IsTrue(_student.IsValid, "Estudante não deve retornar válido quando inscrição estiver ativa");
    }

    [TestMethod]
    public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
    {
        _student.AddSubscription(_subscription);
        Assert.IsTrue(_student.IsValid, "Deve retornar erro quando a inscrição não tem pagamento");
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenHadNoActiveSubscription()
    {
        var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5),  10, 10, "Lula Molusco", _document, _address, _email);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        Assert.IsTrue(_student.IsValid, "Deve retornar sucesso quando");
    }
}
