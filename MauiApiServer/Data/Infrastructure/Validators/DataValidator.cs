using MauiApiServer.Data.Core.Interfaces;
using MauiApiServer.Data.Core.Models;
using MauiApiServer.Data.Core.ViewModels;

namespace MauiApiServer.Data.Infrastructure.Validators
{
    public class DataValidator : IDataValidator
    {
        public async Task<List<PersonViewModel>> ValidateData(List<Person> people)
        {
            var tasks = people.Select(ValidateData).ToList();
            var peopleVM = await Task.WhenAll(tasks);
            return peopleVM.ToList();
        }

        public async Task<PersonViewModel> ValidateData(Person person)
        {
            var personViewModel = new PersonViewModel
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                Country = person.Country,
                Age = person.Age,
                Date = person.Date,
                Id = person.Id,
                Status = IsPersonValid(person) ? ValidationStatus.Valid : ValidationStatus.InValid
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
