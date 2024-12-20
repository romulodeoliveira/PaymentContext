using PaymentContext.Domain.Enums;

namespace PaymentContext.Domain.ValueObjects;

public class Document
{
    public Document(string documentNumber, EDocumentType documentType)
    {
        Number = documentNumber;
        Type = documentType;
    }
    
    public string Number { get; private set; }
    public EDocumentType Type { get; private set; }
}