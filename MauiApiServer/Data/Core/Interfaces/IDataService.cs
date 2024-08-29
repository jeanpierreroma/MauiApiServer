namespace MauiApiServer.Data.Core.Interfaces
{
    public interface IDataService
    {
        Task<bool> CheckDataForEmpry(List<List<string>> data);
        Task<string> ProcessFileAsync(IFormFile file);
        Task<string> ValidateDataAsync(List<List<string>> data);
        Task<string> SaveDataAsync(List<List<string>> data);
    }
}
