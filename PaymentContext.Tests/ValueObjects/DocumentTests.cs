using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow("73110614606387")]
        [DataRow("95285639894038")]
        [DataRow("78765048321528")]
        public void ShouldReturnErrorWhenCNPJIsInvalid(string cnpj)
        {
            // Arrange
            var doc = new Document(cnpj, EDocumentType.CNPJ);

            // Act & Assert
            Assert.IsFalse(doc.IsValid, "CNPJ inválido não deve ser válido.");
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("59519217000155")]
        [DataRow("37808982000152")]
        [DataRow("47208397000175")]
        public void ShouldReturnSuccessWhenCNPJIsValid(string cnpj)
        {
            // Arrange
            var doc = new Document(cnpj, EDocumentType.CNPJ);

            // Act & Assert
            Assert.IsTrue(doc.IsValid, "CNPJ válido deve ser considerado válido.");
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("12345678900")]
        [DataRow("98765432112")]
        [DataRow("11111111111")]
        public void ShouldReturnErrorWhenCPFIsInvalid(string cpf)
        {
            // Arrange
            var doc = new Document(cpf, EDocumentType.CPF);

            // Act & Assert
            Assert.IsFalse(doc.IsValid, "CPF inválido não deve ser válido.");
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("30460503057")]
        [DataRow("86955487086")]
        [DataRow("55704172067")]
        public void ShouldReturnSuccessWhenCPFIsValid(string cpf)
        {
            // Arrange
            var doc = new Document(cpf, EDocumentType.CPF);

            // Act & Assert
            Assert.IsTrue(doc.IsValid, "CPF válido deve ser considerado válido.");
        }
    }
}