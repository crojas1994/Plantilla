using System.Text.RegularExpressions;
using VentanillaUnica.Tramites.Domain.IResources;
using VentanillaUnica.Tramites.Domain.IServices;

namespace VentanillaUnica.Tramites.Domain.Services
{
    public class ParameterDomainService : IParameterDomainService
    {
        private readonly IConstantsProvider constantsProvider;
        public ParameterDomainService(IConstantsProvider constantsProvider)
        {
            this.constantsProvider = constantsProvider;
        }

        public bool ValidateIdentifier(string identifier)
        {
            var regex = new Regex(constantsProvider.AlphanumericExpression ?? "");

            var match = regex.Match(identifier);

            return match.Success;
        }
    }
}
