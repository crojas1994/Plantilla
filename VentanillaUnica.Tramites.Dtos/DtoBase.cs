namespace VentanillaUnica.Tramites.Dtos
{
    public class DtoBase<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}
