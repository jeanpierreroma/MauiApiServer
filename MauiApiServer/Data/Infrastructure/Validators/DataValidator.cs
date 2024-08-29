using MauiApiServer.Data.Core.Interfaces;

namespace MauiApiServer.Data.Infrastructure.Validators
{
    public class DataValidator : IDataValidator
    {
        public async Task ValidateData(List<List<string>> data)
        {
            int rows = data.Count;
            int columns = data[0].Count;

            await Task.Run(() =>
            {
                for (int i = 0; i < rows; i++)
                {
                    bool isValid = true;
                    for (int j = 0; j < columns - 1; j++)
                    {
                        if (string.IsNullOrWhiteSpace(data[i][j]))
                        {
                            isValid = false;
                            break;
                        }
                    }
                    data[i][^1] = isValid.ToString();
                }
            });
        }
    }
}
