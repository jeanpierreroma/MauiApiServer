using MauiApiServer.Data.Core.Interfaces;
using MauiApiServer.Data.Core.Models;
using MauiApiServer.Data.Core.ViewModels;

namespace MauiApiServer.Data.Infrastructure.Validators
{
    public class DataValidator : IDataValidator
    {
        public async Task<List<PersonViewModel>> ValidateDataAsync(List<Person> people)
        {
            var tasks = people.Select(ValidateDataAsync).ToList();
            var peopleVM = await Task.WhenAll(tasks);
            return peopleVM.ToList();
        }

        public async Task<PersonViewModel> ValidateDataAsync(Person? person)
        {
            var personViewModel = new PersonViewModel
            {
                FirstName = person?.FirstName ?? string.Empty,
                LastName = person?.LastName ?? string.Empty,
                Gender = person?.Gender ?? string.Empty,
                Country = person?.Country ?? string.Empty,
                Age = person?.Age ?? 0,
                Date = person?.Date ?? DateTime.UtcNow,
                Id = person?.Id ?? -1,
                Status = person == null || !IsPersonValid(person) ? ValidationStatus.InValid : ValidationStatus.Valid
            };

            return await Task.FromResult(personViewModel);
        }

        private static bool IsPersonValid(Person person)
        {
            return !string.IsNullOrEmpty(person.FirstName) &&
                   !string.IsNullOrEmpty(person.LastName) &&
                   !string.IsNullOrEmpty(person.Gender) &&
                   !string.IsNullOrEmpty(person.Country) &&
                   person.Age > 0 &&
                   person.Date != DateTime.MinValue;
        }
    }
}
