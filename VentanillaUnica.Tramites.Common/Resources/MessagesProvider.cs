using System.Resources;
using VentanillaUnica.Tramites.Domain.IResources;

namespace VentanillaUnica.Tramites.Common.Resources
{
    public class MessagesProvider : IMessagesProvider
    {
        private readonly ResourceManager _resourceManager;

        public MessagesProvider()
        {
            this._resourceManager = new ResourceManager("VentanillaUnica.Tramites.Common.Resources.Messages", typeof(ConstantsProvider).Assembly);
        }

        public string? InvalidParameter => _resourceManager.GetString("InvalidParameter");
    }
}
