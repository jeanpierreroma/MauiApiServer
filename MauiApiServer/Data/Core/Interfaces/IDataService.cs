using MauiApiServer.Data.Core.ViewModels;

namespace MauiApiServer.Data.Core.Interfaces
{
    public interface IDataService
    {
        Task<List<PersonViewModel>?> ProcessFileAsync(IFormFile file);
        Task<PersonViewModel?> UpdatePerson(PersonViewModel person);
        Task<string> SaveDataAsync(List<PersonViewModel> data);
    }
}
