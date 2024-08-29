using MauiApiServer.Data.Core.Models;

namespace MauiApiServer.Data.Core.Interfaces
{
    public interface IDataParser
    {
        List<Person> ParseData(List<List<string>> data);
    }
}
