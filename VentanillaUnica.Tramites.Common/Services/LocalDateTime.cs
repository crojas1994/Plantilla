using VentanillaUnica.Tramites.Common.Extensions;
using VentanillaUnica.Tramites.Common.IServices;

namespace VentanillaUnica.Tramites.Common.Services
{
    public class LocalDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now.GetColombiaDateNow();
    }
}
