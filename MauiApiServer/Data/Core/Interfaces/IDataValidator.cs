using MauiApiServer.Data.Core.Models;
using MauiApiServer.Data.Core.ViewModels;

namespace MauiApiServer.Data.Core.Interfaces
{
    public interface IDataValidator
    {
        Task<List<PersonViewModel>> ValidateDataAsync(List<Person> data);
        Task<PersonViewModel?> ValidateDataAsync(Person? person); 
    }
}
