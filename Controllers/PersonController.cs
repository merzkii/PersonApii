using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;

namespace PersonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IConfiguration Config;
        public PersonController(IConfiguration config)
        {
            Config = config;
        }
        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetPersons()
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var persons = await connection.QueryAsync<Person>("select * from dbo.Person");
            return Ok(persons);
        }
        [HttpGet("{personId}")]
        public async Task<ActionResult<Person>> GetPerson(int personiId)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var person = await connection.QueryFirstAsync<Person>("select * from dbo.Person where id=@Id",
                new { Id = personiId });
            return Ok(person);

        }
        [HttpPost]
        public async Task<ActionResult<List<Person>>> CreatePersons(Person person)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("insert into dbo.Person (FirstName,LastName,Age,PersonalNumber,Phone,City)values(@FirstName,@LastName,@Age,@PersonalNumber,@Phone,@City)", person);

            return Ok(await GetPersons());
        }
        [HttpDelete("{personId}")]
        public async Task<ActionResult<List<Person>>> DeleteePersons(int personId)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("delete from person where id=@Id", new { Id = personId });

            return Ok(await GetPersons());


        }

    }
}
            


            
    

