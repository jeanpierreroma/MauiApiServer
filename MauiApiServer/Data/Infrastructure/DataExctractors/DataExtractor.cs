using MauiApiServer.Data.Core.Interfaces;
using MauiApiServer.Data.Infrastructure.DataExctractors.Interfaces;

namespace MauiApiServer.Data.Infrastructure.DataExctractors
{
    public class DataExtractor : IDataExtractor
    {
        private readonly IExcelDataReader _dataReader;

        public DataExtractor(IExcelDataReader dataReader)
        {
            _dataReader = dataReader;
        }

        public async Task<List<List<string>>> ExtractData(IFormFile file)
        {
            using var stream = file.OpenReadStream();

            using StreamReader reader = new StreamReader(stream);

            return await _dataReader.ReadAllData(stream);
        }
    }
}
