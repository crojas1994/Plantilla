using System.Resources;
using VentanillaUnica.Tramites.Domain.IResources;

namespace VentanillaUnica.Tramites.Common.Resources
{
    public class ConstantsProvider : IConstantsProvider
    {
        private readonly ResourceManager _resourceManager;

        public ConstantsProvider()
        {
            this._resourceManager = new ResourceManager("VentanillaUnica.Tramites.Common.Resources.Constants", typeof(ConstantsProvider).Assembly);
        }

        public string? AlphanumericExpression => _resourceManager.GetString("AlphanumericExpression");
    }
}
