using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Document : ValueObject
{
    public Document(string documentNumber, EDocumentType documentType)
    {
        Number = documentNumber;
        Type = documentType;

        if(string.IsNullOrEmpty(Number))
        {
            AddNotification("Document.Number", "Número inválido");
        }
    }
    
    public string Number { get; private set; }
    public EDocumentType Type { get; private set; }
}