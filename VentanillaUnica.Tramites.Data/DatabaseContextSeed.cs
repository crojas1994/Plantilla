using VentanillaUnica.Tramites.Domain.Entities;
using VentanillaUnica.Tramites.Domain.Repositories;

namespace VentanillaUnica.Tramites.Data
{
    public static class DatabaseContextSeed
    {
        public static void EnsureSeeded(this IUnitOfWork context)
        {
            EnsureParameters(context);

            context.Commit();
        }


        private static void EnsureParameters(IUnitOfWork context)
        {
            if (!context.GetSet<Guid, Parameter>().Any(prop => prop.Indentifier.Equals("DefaultParameter")))
            {
                context.GetSet<Guid, Parameter>().Add(new Parameter
                {
                    Id = Guid.NewGuid(),
                    Indentifier = "DefaultParameter",
                    Value = "123",
                    Observation = "Ejemplo de como configurar un parámetro como semilla de la aplicación",
                    DateRegistered = DateTime.Now,
                    RegistrationUser = "crojas"
                });
            }
        }
    }
}
