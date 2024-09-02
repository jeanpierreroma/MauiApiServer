using MauiApiServer.Data.Core.Interfaces;
using MauiApiServer.Data.Core.Models;
using MauiApiServer.Data.Core.ViewModels;
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

        public async Task<List<PersonViewModel>?> ProcessFileAsync(IFormFile file)
        {
            // Extract data
            var data = await _dataExtractor.ExtractData(file);

            // Parse data
            var people = _dataParser.ParseData(data);

            // Validate data
            return await ValidateDataAsync(people);
        }

        public async Task<PersonViewModel?> UpdatePerson(Person person)
        {
            return await ValidateDataAsync(person);
        }

        public async Task<string> SaveDataAsync(List<Person> data)
        {
            if (await CheckDataForEmpry(data))
            {
                return "Data is emply!";
            }

            // Validate data
            var people = (await _dataValidator.ValidateDataAsync(data))
                .Where(pvm => pvm.Status == ValidationStatus.Valid)
                .Select(pvm => new Person
                {
                    Id = pvm.Id,
                    FirstName = pvm.FirstName,
                    LastName = pvm.LastName,
                    Gender = pvm.Gender,
                    Country = pvm.Country,
                    Age = pvm.Age,
                    Date = pvm.Date
                });


            var uniquesPeople = FindUniquePeople(people);

            try
            {
                foreach (var person in uniquesPeople)
                {
                    var existingPerson = await _context.Persons
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Id == person.Id);

                    // if exist, then update, unless add new
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


        private async Task<bool> CheckDataForEmpry(List<Person> data)
        {
            return await Task.FromResult(data == null || data.Count == 0);
        }
        private async Task<List<PersonViewModel>?> ValidateDataAsync(List<Person> data)
        {
            if (await CheckDataForEmpry(data))
            {
                return null;
            }

            return await _dataValidator.ValidateDataAsync(data);
        }
        private async Task<PersonViewModel?> ValidateDataAsync(Person? person)
        {
            return await _dataValidator.ValidateDataAsync(person);
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
