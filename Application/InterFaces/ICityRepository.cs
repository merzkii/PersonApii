using Application.CreateDTO;
using Application.UpdateDTO;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterFaces
{
    public interface ICityRepository
    {
        Task<List<City>> CreateCity(CreateCityDTO city);
        Task<List<City>> GetAllCities();
        Task<City> GetCity(int cityId);
        Task<List<City>> UpdateCity(CityDto city);
        Task <List<City>> DeleteCity(int cityId);
    }
}
