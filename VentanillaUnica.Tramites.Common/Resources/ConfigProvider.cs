using Microsoft.Extensions.Configuration;
using VentanillaUnica.Tramites.Domain.IResources;

namespace VentanillaUnica.Tramites.Common.Resources
{
    public class ConfigProvider : IConfigProvider
    {
        private readonly IConfiguration configuration;

        public ConfigProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string SchemaName => configuration.GetSection("ConnectionStrings:SchemaName").Value;
    }
}
