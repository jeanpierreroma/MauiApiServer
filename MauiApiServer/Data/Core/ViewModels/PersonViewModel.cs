using MauiApiServer.Data.Core.Models;

namespace MauiApiServer.Data.Core.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public int? Age { get; set; }
        public DateTime? Date { get; set; }
        public ValidationStatus Status { get; set; }
    }
}
