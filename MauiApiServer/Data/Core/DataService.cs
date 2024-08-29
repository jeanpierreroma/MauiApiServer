using MauiApiServer.Data.Core.Interfaces;
using MauiApiServer.Data.Core.Models;
using MauiApiServer.Data.Infrastructure.DataParsing;
using MauiApiServer.Data.Infrastructure.Validators;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MauiApiServer.Data.Core
{
    public class DataService : IDataService
    {
        private readonly AppDbContext _context;

        private readonly IDataExtractor _dataExtractor;
        private readonly IDataValidator _dataValidator;
        private readonly IDataParser _dataParser;

        public DataService(
            AppDbContext context,
            IDataExtractor dataExtractor,
            IDataValidator dataValidator,
            IDataParser dataParser)
        {
            _context = context;
            _dataExtractor = dataExtractor;
            _dataValidator = dataValidator;
            _dataParser = dataParser;
        }

        public async Task<bool> CheckDataForEmpry(List<List<string>> data)
        {
            return await Task.FromResult(data != null && data.Count > 0);
        }

        public async Task<string> ProcessFileAsync(IFormFile file)
        {
            // Extract data
            var data = await _dataExtractor.ExtractData(file);

            // Validate data
            return await ValidateDataAsync(data);
        }

        public async Task<string> ValidateDataAsync(List<List<string>> data)
        {
            await _dataValidator.ValidateData(data);

            return JsonSerializer.Serialize(data);
        }

        public async Task<string> SaveDataAsync(List<List<string>> data)
        {
            // Validate data
            await _dataValidator.ValidateData(data);

            // Parse data
            var people = _dataParser.ParseData(data);

            var uniquesPeople = FindUniquePeople(people);

            try
            {
                foreach (var person in uniquesPeople)
                {
                    var existingPerson = await _context.Persons
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Id == person.Id);

                    if (existingPerson != null)
                    {
                        _context.Entry(existingPerson).CurrentValues.SetValues(person);
                    }
                    else
                    {
                        _context.Persons.Add(person);
                    }
                }

                await _context.SaveChangesAsync();
                return "Data was successfully saved into database";
            }
            catch (Exception ex)
            {
                return $"An error occurred while saving data: {ex.Message}";
            }
        }

        private IEnumerable<Person> FindUniquePeople(IEnumerable<Person> people)
        {
            var uniquePeople = people
            .GroupBy(p => p.Id)
            .Select(g => g.First());


            return uniquePeople;
        }
    }
}
