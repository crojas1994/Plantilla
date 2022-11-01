using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VentanillaUnica.Tramites.Application.IServices;
using VentanillaUnica.Tramites.Application.Mapping;
using VentanillaUnica.Tramites.Application.Services;
using VentanillaUnica.Tramites.Common.IServices;
using VentanillaUnica.Tramites.Common.Resources;
using VentanillaUnica.Tramites.Common.Services;
using VentanillaUnica.Tramites.Data;
using VentanillaUnica.Tramites.Data.Repositories;
using VentanillaUnica.Tramites.Domain.IResources;
using VentanillaUnica.Tramites.Domain.IServices;
using VentanillaUnica.Tramites.Domain.Repositories;
using VentanillaUnica.Tramites.Domain.Services;

namespace VentanillaUnica.Tramites.Application.Extensions
{
    public static class DependencyInjectionExtencion
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            RegisterResources(services);
            RegisterAplicationType(services);
            RegisterDomainType(services);
            RegisterRepositoryType(services);
        }

        public static void RegisterResources(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var mapperConfiguration = new MapperConfiguration(configurationExpression =>
            {
                configurationExpression.AddProfile(new MappingProfile());
            });

            services.AddSingleton(prop => mapperConfiguration.CreateMapper());
            services.AddSingleton<IConstantsProvider, ConstantsProvider>();
            services.AddSingleton<IConfigProvider, ConfigProvider>();
            services.AddSingleton<IMessagesProvider, MessagesProvider>();
            services.AddSingleton<IDateTime, LocalDateTime>();
        }

        public static void RegisterAplicationType(IServiceCollection services)
        {
            services.AddScoped<IParameterAplicationService, ParameterAplicationService>();
        }

        public static void RegisterDomainType(IServiceCollection services)
        {
            services.AddScoped<IParameterDomainService, ParameterDomainService>();
        }

        public static void RegisterRepositoryType(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, DatabaseContext>();
            services.AddScoped<IParameterRepository, ParameterRepository>();
        }
    }
}
