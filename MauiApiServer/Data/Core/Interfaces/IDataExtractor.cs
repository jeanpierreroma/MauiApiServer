namespace MauiApiServer.Data.Core.Interfaces
{
    public interface IDataExtractor
    {
        Task<List<List<string>>> ExtractData(IFormFile file);
    }
}
