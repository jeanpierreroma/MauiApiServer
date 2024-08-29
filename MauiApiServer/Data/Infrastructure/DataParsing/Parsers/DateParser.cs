using MauiApiServer.Data.Infrastructure.DataParsing.Interfaces;

namespace MauiApiServer.Data.Infrastructure.DataParsing.Parsers
{
    public class DateParser : IDateParser
    {
        public DateTime Parse(string date)
        {
            // DD/MM/YYYY
            var parts = date.Split("/");

            int day = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int year = int.Parse(parts[2]);

            return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
        }
    }
}
