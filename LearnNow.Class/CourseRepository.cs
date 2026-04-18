using LearnNow.Class.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNow.Class
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LearnNowDB db;

        public CourseRepository(LearnNowDB db)
        {
            this.db = db;
        }

        /// <summary>
        /// Permite crear un curso en la base de datos, devuelve el id del curso creado
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> Create(CourseModel course)
        {
            if (course == null) throw new ArgumentNullException("Se requiere un curso. ");
            await db.Courses.AddAsync(course);
            await db.SaveChangesAsync();
            return course.Id;
        }

        public void Delete(int id)
        {
            var c = new CourseModel { Id = id };
            db.Courses.Attach(c);
            db.Remove(c);
            db.SaveChanges();
        }

        public async Task<List<CourseModel>> GetAll()
        {
            return await db.Courses.ToListAsync();
        }

        public async Task<CourseModel> GetById(int id)
        {
            var course=await db.Courses.FirstOrDefaultAsync(c=>c.Id == id);
            return course!;
        }

        public void Update(CourseModel course)
        {
            var c=db.Courses.FirstOrDefault(c=> c.Id == course.Id);
            if(course == null)
            {
                throw new ArgumentException("El curso no existe. ");
            }
            c.Title = course.Title;
            c.Price = course.Price;
            c.Duration = course.Duration;
            c.Description = course.Description;
            db.SaveChanges();
        }
    }
}
