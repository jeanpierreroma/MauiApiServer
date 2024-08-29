namespace MauiApiServer.Data.Core.Interfaces
{
    public interface IDataValidator
    {
        Task ValidateData(List<List<string>> data);
    }
}
