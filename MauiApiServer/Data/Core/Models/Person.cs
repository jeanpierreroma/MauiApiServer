using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MauiApiServer.Data.Core.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime Date { get; set; }
    }
}
