using MauiApiServer.Data.Core.Models;
using MauiApiServer.Data.Core.ViewModels;

namespace MauiApiServer.Data.Core.Interfaces
{
    public interface IDataService
    {
        Task<List<PersonViewModel>?> ProcessFileAsync(IFormFile file);
        Task<PersonViewModel?> UpdatePerson(int id, Person person);
        Task<PersonViewModel?> GetPersonById(int id);
        Task<string> DeletePerson(int id);

        Task<string> SaveDataAsync(List<List<string>> data);
    }
}
