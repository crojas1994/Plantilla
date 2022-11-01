using VentanillaUnica.Tramites.Domain.Entities;

namespace VentanillaUnica.Tramites.Domain.Repositories
{
    public interface IParameterRepository : IRepository<Guid, Parameter>
    {
        Task<Parameter?> GetByIdentifier(string identifier);
    }
}
