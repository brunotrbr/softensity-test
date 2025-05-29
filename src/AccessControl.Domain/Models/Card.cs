using System.ComponentModel.DataAnnotations;

namespace AccessControl.Domain.Models
{
    public class Card
    {
        [Key]
        public string Id { get; set; }
        public int Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public List<int> DoorsNumbersWithAccess { get; set; } = [];
    }
}