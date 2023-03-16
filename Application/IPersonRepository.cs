using Microsoft.AspNetCore.Mvc;
using PersonApi;

namespace Application
{
    public interface IPersonRepsoitory
    {
        Task<ActionResult<List<Person>>>CreatePerson(Person person);
        Task<ActionResult<List<Person>>> GetAllPersons();
        Task<ActionResult<Person>> GetPerson(int personid);
        Task<ActionResult<List<Person>>>UpdatePerson(Person person);
        Task<ActionResult<List<Person>>>DeletePerson(int personid);
    }
}