using MauiApiServer.Data.Core.Models;
using MauiApiServer.Data.Core.ViewModels;

namespace MauiApiServer.Data.Core.Interfaces
{
    public interface IDataParser
    {
        List<Person> ParseData(List<List<string>> data);
    }
}
