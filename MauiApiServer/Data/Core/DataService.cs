using MauiApiServer.Data.Core.Interfaces;
using MauiApiServer.Data.Core.Models;
using MauiApiServer.Data.Core.ViewModels;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PersonViewModel?> UpdatePerson(PersonViewModel person)
        {
            return await ValidateDataAsync(person);
        }

        public async Task<string> SaveDataAsync(List<PersonViewModel> data)
        {
            if (CheckDataForEmpry(data))
            {
                return "Data is emply!";
            }

            // Valid data
            var people = data
                .Where(pvm => pvm.Status == ValidationStatus.Valid)
                .Select(pvm => new Person
                {
                    Id = pvm.Id,
                    FirstName = pvm.FirstName!,
                    LastName = pvm.LastName!,
                    Gender = pvm.Gender!,
                    Country = pvm.Country!,
                    Age = pvm.Age!.Value,
                    Date = pvm.Date!.Value
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


        private bool CheckDataForEmpry(List<PersonViewModel> data)
        {
            return data is null || data.Count == 0;
        }
        private async Task<List<PersonViewModel>?> ValidateDataAsync(List<Person> data)
        {
            return await _dataValidator.ValidateDataAsync(data);
        }
        private async Task<PersonViewModel?> ValidateDataAsync(PersonViewModel? person)
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
