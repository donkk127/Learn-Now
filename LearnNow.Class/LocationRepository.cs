using LearnNow.Class.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNow.Class
{
    public class LocationRepository : ILocationRepository
    {
        public Task<int> Create(LocationModel location)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<LocationModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<LocationModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(LocationModel location)
        {
            throw new NotImplementedException();
        }
    }
}
