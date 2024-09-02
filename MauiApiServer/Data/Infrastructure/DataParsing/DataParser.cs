using MauiApiServer.Data.Core.Interfaces;
using MauiApiServer.Data.Core.Models;
using MauiApiServer.Data.Infrastructure.DataParsing.Interfaces;

namespace MauiApiServer.Data.Infrastructure.DataParsing
{
    public class DataParser : IDataParser
    {
        private readonly IPersonParser _personParser;

        public DataParser(IPersonParser personParser)
        {
            _personParser = personParser;
        }

        public List<Person> ParseData(List<List<string>> data)
        {
            var people = new List<Person>();

            foreach (var row in data)
            {
                var neededData = row.Skip(1).Take(7).ToArray();

                var person = _personParser.Parse(neededData);

                people.Add(person);
            }

            return people;
        }
    }
}
