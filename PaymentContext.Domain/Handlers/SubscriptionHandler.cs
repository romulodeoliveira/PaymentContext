using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler :
    Notifiable<Notification>,
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>
{
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public ICommandResult Handler(CreateBoletoSubscriptionCommand command)
    {
        // fail fast validate
        command.Validate();
        if(!command.IsValid)
        {
            return new CommandResult(false, "não foi possível realizar sua assinatura");
        }

        // verificar se o documento já está cadastrado
        if(_repository.DocumentExists(command.Document))
        {
            AddNotification("Document", "Este CPF já está em uso");
        }

        // verificar se o e-mail já está cadastrado
        if(_repository.EmailExists(command.Email))
        {
            AddNotification("Email", "Este e-mail já está em uso");
        }

        // gerar os VOs
        var name = new Name("Bob Esponja", "Calça Quadrada");
        var document = new Document("55704172067", EDocumentType.CPF);
        var email = new Email("bob@email.com");
        var address = new Address(
            command.Street, 
            command.Number, 
            command.Neighborhood, 
            command.City, 
            command.State, 
            command.Country, 
            command.ZipCode
        );

        // gerar as entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(
            command.BarCode, 
            command.BoletoNumber, 
            command.PaidDate, 
            command.ExpireDate, 
            command.Total, 
            command.TotalPaid, 
            command.Payer, 
            new Document(
                command.PayerDocument, 
                command.PayerDocumentType
            ), 
            address, 
            email
        );

        // relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // aplicar as validações
        AddNotifications(name, document, email, address, student, subscription, payment);

        // checar as notificaçoes
        if (IsValid == false)
        {
            return new CommandResult(false, "Não foi possível realizar sua assinatura.");
        }

        // salvar as informações
        _repository.CreateSubscription(student);

        // enviar e-mail de boas vindas
        _emailService.Send(
            student.Name.ToString(), 
            student.Email.Address, 
            "Bem vindo!", 
            "Sua assinatura foi criada!");

        // retornar informações
        return new CommandResult(true, "assinatura realizada com sucesso");
    }

    public ICommandResult Handler(CreatePayPalSubscriptionCommand command)
    {
        // verificar se o documento já está cadastrado
        if(_repository.DocumentExists(command.Document))
        {
            AddNotification("Document", "Este CPF já está em uso");
        }

        // verificar se o e-mail já está cadastrado
        if(_repository.EmailExists(command.Email))
        {
            AddNotification("Email", "Este e-mail já está em uso");
        }

        // gerar os VOs
        var name = new Name("Bob Esponja", "Calça Quadrada");
        var document = new Document("55704172067", EDocumentType.CPF);
        var email = new Email("bob@email.com");
        var address = new Address(
            command.Street, 
            command.Number, 
            command.Neighborhood, 
            command.City, 
            command.State, 
            command.Country, 
            command.ZipCode
        );

        // gerar as entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new PayPalPayment(
            command.TransactionCode, 
            command.PaidDate, 
            command.ExpireDate, 
            command.Total, 
            command.TotalPaid, 
            command.Payer, 
            new Document(
                command.PayerDocument, 
                command.PayerDocumentType
            ), 
            address, 
            email
        );

        // relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // aplicar as validações
        AddNotifications(name, document, email, address, student, subscription, payment);

        // checar as notificaçoes
        if (IsValid == false)
        {
            return new CommandResult(false, "Não foi possível realizar sua assinatura.");
        }

        // salvar as informações
        _repository.CreateSubscription(student);

        // enviar e-mail de boas vindas
        _emailService.Send(
            student.Name.ToString(), 
            student.Email.Address, 
            "Bem vindo!", 
            "Sua assinatura foi criada!");

        // retornar informações
        return new CommandResult(true, "assinatura realizada com sucesso");
    }
}
