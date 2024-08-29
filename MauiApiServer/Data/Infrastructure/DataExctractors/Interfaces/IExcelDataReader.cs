namespace MauiApiServer.Data.Infrastructure.DataExctractors.Interfaces
{
    public interface IExcelDataReader
    {
        Task<List<List<string>>> ReadAllData(Stream stream);
    }
}
