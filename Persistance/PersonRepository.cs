using Application;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PersonApi;
using System.Data.SqlClient;

namespace Persistance
{
    public class PersonRepository:IPersonRepsoitory
    {
        private readonly IConfiguration Config;
        public PersonRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<ActionResult<List<Person>>> CreatePerson(Person person)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var create=await connection.ExecuteAsync("insert into dbo.Person (FirstName,LastName,Age,PersonalNumber,Phone,City)values(@FirstName,@LastName,@Age,@PersonalNumber,@Phone,@City)", person);

            return await GetAllPersons();
        }

        public async Task<ActionResult<List<Person>>> GetAllPersons()
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            IEnumerable<Person> persons = await connection.QueryAsync<Person>("select * from dbo.Person");

            return persons.ToList();
        }

        //private static async Task<IEnumerable<Person>> SelectAllPersons(SqlConnection connection)
        //{
        //    return  await connection.QueryAsync<Person>("select * from dbo.Person");
        //}

        public async Task<ActionResult<Person>> GetPerson(int personiId)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var person = await connection.QueryFirstAsync<Person>("select * from dbo.Person where id=@Id",
                new { Id = personiId });
            return person;

        }

        public async Task<ActionResult<List<Person>>> UpdatePerson(Person person)
        { 
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var update=await connection.ExecuteAsync("update dbo.Person set FirstName=@FirstName, LastName=@LastName,Age=@Age,PersonalNumber=@PersonalNumber,City=@City,Phone=@Phone, Id=@Id", person);

            return await GetAllPersons();
        }
        public async Task <ActionResult<List<Person>>> DeletePerson(int personId)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var delete=await connection.ExecuteAsync("delete from person where id=@Id", new { Id = personId });

            return await GetAllPersons();


        }
    }
}