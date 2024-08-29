using MauiApiServer.Data.Core.Models;
using MauiApiServer.Data.Infrastructure.DataParsing.Interfaces;

namespace MauiApiServer.Data.Infrastructure.DataParsing.Parsers
{
    public class PersonParser : IPersonParser
    {
        private readonly IDateParser _dateParser;

        public PersonParser(IDateParser dataParser)
        {
            _dateParser = dataParser;
        }
        public Person Parse(string[] data)
        {
            return new Person
            {
                FirstName = data[0],
                LastName = data[1],
                Gender = data[2],
                Country = data[3],
                Age = int.Parse(data[4]),
                Date = _dateParser.Parse(data[5]),
                Id = int.Parse(data[6])
            };
        }
    }
}
