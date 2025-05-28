namespace AccessControl.Domain.Models
{
    public class Card
    {
        public string Id;
        public int Number;
        public string FirstName;
        public string LastName;
        public DateTime ValidFrom;
        public DateTime ValidTo;
        public List<int> DoorsNumbersWithAccess;
    }
}