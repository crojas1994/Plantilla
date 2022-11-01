using VentanillaUnica.Tramites.Dtos;

namespace VentanillaUnica.Tramites.Application.IServices
{
    public interface IParameterAplicationService
    {
        Task<ParameterDto> GetByIdentifierAsync(string identifier);
    }
}
