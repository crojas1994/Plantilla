using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VentanillaUnica.Tramites.Domain.IResources;
using VentanillaUnica.Tramites.Domain.IServices;
using VentanillaUnica.Tramites.Domain.Services;

namespace VentanillaUnica.Tramites.Domain.Test.Services
{
    [TestClass]
    public class ParameterDomainServiceTest
    {
        private Mock<IConstantsProvider> constantsProvider;
        private IParameterDomainService parameterDomainService;

        [TestInitialize]
        public void Initialize()
        {
            constantsProvider = new Mock<IConstantsProvider>();
            parameterDomainService = new ParameterDomainService(constantsProvider.Object);

            constantsProvider.Setup(x => x.AlphanumericExpression).Returns("^[a-zA-Z0-9]+$");
        }

        [TestMethod]
        public void ValidateIdentifierSuccess()
        {
            //Arrange
            var identifier = "DefaultParameter";

            //Act
            var result = parameterDomainService.ValidateIdentifier(identifier);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateIdentifierFail()
        {
            //Arrange
            var identifier = "Default&%$Parameter";

            //Act
            var result = parameterDomainService.ValidateIdentifier(identifier);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
