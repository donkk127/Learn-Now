using LearnNow.Class.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNow.Class
{
    public interface ICourseRepository
    {
        Task<int> Create(CourseModel course);

        void Delete(int id);

        Task<List<CourseModel>> GetAll();

        Task<CourseModel> GetById(int id);

        void Update(CourseModel course);
    }
}
