using MauiApiServer.Data.Core.Models;

namespace MauiApiServer.Data.Infrastructure.DataParsing.Interfaces
{
    public interface IPersonParser
    {
        Person Parse(string[] data);
    }
}
