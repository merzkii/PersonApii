using Domain;

namespace PersonApi
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PersonalNumber { get; set; }
        public string Phone { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
    }
}
