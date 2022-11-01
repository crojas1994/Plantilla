using VentanillaUnica.Tramites.Domain.Entities;
using VentanillaUnica.Tramites.Domain.Repositories;

namespace VentanillaUnica.Tramites.Data.Repositories
{
    public class ParameterRepository : Repository<Guid, Parameter>, IParameterRepository
    {
        public ParameterRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Parameter?> GetByIdentifier(string identifier)
        {
            var parameters = await GetAllAsync(prop => prop.Indentifier.Equals(identifier));
            return parameters.FirstOrDefault();
        }
    }
}
