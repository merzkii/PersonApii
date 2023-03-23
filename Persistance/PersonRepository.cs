using Application;
using Application.DTO;
using Dapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PersonApi;
using System.Data.SqlClient;

namespace Persistance
{
    public class PersonRepository : IPersonRepsoitory
    {
        private readonly IConfiguration Config;
        public PersonRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<List<Person>> CreatePerson(CreatePersonDTO person)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var create = await connection.ExecuteAsync("insert into dbo.Person (FirstName,LastName,Age,PersonalNumber,Phone,CityId)values(@FirstName,@LastName,@Age,@PersonalNumber,@Phone,@CityId)", person);

            return await GetAllPersons();
        }

        public async Task<List<Person>> GetAllPersons()
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            IEnumerable<Person> persons = await SelectAllPersons(connection);

            return persons.ToList();
        }

        private  async Task<IEnumerable<Person>> SelectAllPersons(SqlConnection connection)
        {
            var a = await connection.QueryAsync<Person>("select * from dbo.Person");
            return a;
        }

        public async Task<Person> GetPerson(int personiId)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            
                connection.Open();

                var query = @"
                SELECT p.*, c.* 
                 FROM Person p 
                JOIN City c ON p.CityId = c.Id 
                WHERE p.Id = @Id";

            var person = await connection.QueryAsync<Person, City, Person>(query, (person, city) => { person.City = city; return person; }, new { Id = personiId });
                

            return person.SingleOrDefault();
            connection.Close();
        }

        public async Task<List<Person>> UpdatePerson(PersonDTO person)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var update = await connection.ExecuteAsync("update dbo.Person set FirstName=@FirstName, LastName=@LastName,Age=@Age,PersonalNumber=@PersonalNumber,CityId=@CityId,Phone=@Phone where Id=@Id", person);

            var persons = await SelectAllPersons(connection);
            return persons.ToList();
        }
        public async Task<List<Person>> DeletePerson(int personId)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var delete = await connection.ExecuteAsync("delete from person where id=@Id", new { Id = personId });
            var persons = await SelectAllPersons(connection);
            return persons.ToList();


        }
    }
}