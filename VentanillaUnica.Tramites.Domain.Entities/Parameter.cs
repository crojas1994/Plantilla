namespace VentanillaUnica.Tramites.Domain.Entities
{
    public class Parameter : EntityBase<Guid>
    {
        public string Indentifier { get; set; }
        public string Value { get; set; }
        public string Observation { get; set; }
        public string RegistrationUser { get; set; }
        public DateTime DateRegistered { get; set; }
        public string? UserUpdated { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
}
