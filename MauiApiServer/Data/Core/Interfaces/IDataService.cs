using MauiApiServer.Data.Core.Models;
using MauiApiServer.Data.Core.ViewModels;

namespace MauiApiServer.Data.Core.Interfaces
{
    public interface IDataService
    {
        Task<List<PersonViewModel>?> ProcessFileAsync(IFormFile file);
        Task<PersonViewModel?> UpdatePerson(Person person);
        //Task<PersonViewModel?> GetPersonById(int id);
        //Task<string> DeletePerson(int id);

        Task<string> SaveDataAsync(List<Person> data);
    }
}

// Get data from file -> validate it -> send to front
// Get data from front -> validate it -> send to front
// Get data
