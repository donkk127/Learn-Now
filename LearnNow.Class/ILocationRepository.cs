using LearnNow.Class.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNow.Class
{
    public interface ILocationRepository
    {
        Task<int> Create(LocationModel location);

        void Delete(int id);

        Task<List<LocationModel>> GetAll();

        Task<LocationModel> GetById(int id);

        void Update(LocationModel location);
    }
}
