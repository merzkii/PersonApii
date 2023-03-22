using Application.CreateDTO;
using Application.InterFaces;
using Application.UpdateDTO;
using Dapper;
using Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class CityRepository : ICityRepository
    {
        private readonly IConfiguration Config;
        public CityRepository(IConfiguration config)
        {
            Config = config;
        }
        public async Task<List<City>> CreateCity(CreateCityDTO city)
        {
            using var connection= new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var createCity = await connection.ExecuteAsync("insert into dbo.City(Name) Values(@Name)", city);
            return await GetAllCities();
        }

        public async Task<List<City>> DeleteCity(int cityId)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var delete = await connection.ExecuteAsync("delete from City where id=@Id", new { Id = cityId });
            var cities=await SelectAllCities(connection);
            return cities.ToList();
        }

        public async Task<List<City>> GetAllCities()
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            IEnumerable<City> cities = await SelectAllCities(connection);

            return cities.ToList();
        }

        public async Task<City> GetCity(int cityId)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var city = await connection.QueryFirstAsync<City>("select * from dbo.Person where id=@Id",
               new { Id = cityId });
            return city;
        }

        public async Task<List<City>> UpdateCity(CityDto city)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var update = await connection.ExecuteAsync("update dbo.City set Name=@Name, Id=@Id where Id=@Id", city);

            var cities = await SelectAllCities(connection);
            return cities.ToList();
        }



        private async Task<IEnumerable<City>> SelectAllCities(SqlConnection connection)
        {
            var a = await connection.QueryAsync<City>("select * from dbo.City");
            return a;
        }
    }
}
