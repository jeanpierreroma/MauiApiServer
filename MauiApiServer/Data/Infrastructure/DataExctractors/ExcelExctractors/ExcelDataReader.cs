using MauiApiServer.Data.Infrastructure.DataExctractors.Interfaces;
using OfficeOpenXml;

namespace MauiApiServer.Data.Infrastructure.DataExctractors.ExcelExctractors
{
    public class ExcelDataReader : IExcelDataReader
    {
        public ExcelDataReader()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<List<List<string>>> ReadAllData(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var data = new List<List<string>>();

            using (var package = new ExcelPackage())
            {
                await package.LoadAsync(stream);

                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];

                if (workSheet == null)
                    throw new InvalidOperationException("Worksheet not found.");

                int rowCount = workSheet.Dimension.Rows;
                int columnCount = workSheet.Dimension.Columns;

                data = new List<List<string>>(rowCount - 1);

                for (int row = 2; row <= rowCount; row++)
                {
                    List<string> singleRow = new List<string>(columnCount + 1);
                    for (int col = 1; col <= columnCount; col++)
                    {
                        var cellValue = workSheet.Cells[row, col].Text;
                        singleRow.Add(cellValue);
                    }
                    singleRow.Add(string.Empty);
                    data.Add(singleRow);
                }
            }

            return data;
        }
    }
}
