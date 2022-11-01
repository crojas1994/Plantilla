namespace VentanillaUnica.Tramites.Common.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Retorna la fecha y la hora actual de Colombia
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetColombiaDateNow(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(GetTimeZoneSO()));
        }

        /// <summary>
        /// Calcula la fecha y hora actual de Colombia recibiendo una fecha UTC
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ConvertToColombiaDateTimeFromUtc(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc), TimeZoneInfo.FindSystemTimeZoneById(GetTimeZoneSO()));
        }

        private static string GetTimeZoneSO()
        {
            return Environment.OSVersion.Platform.Equals(PlatformID.Win32NT) ? "SA Pacific Standard Time" : "America/Bogota";
        }
    }
}
