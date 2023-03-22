using Application.DTO;
using Microsoft.AspNetCore.Mvc;
using PersonApi;

namespace Application
{
    public interface IPersonRepsoitory
    {
        Task<List<Person>> CreatePerson(CreatePersonDTO person);
        Task<List<Person>> GetAllPersons();
        Task<Person> GetPerson(int personid);
        Task<List<Person>> UpdatePerson(PersonDTO person);
        Task<List<Person>> DeletePerson(int personid);
    }
}