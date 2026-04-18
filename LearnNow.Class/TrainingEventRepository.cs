using LearnNow.Class.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNow.Class
{
    public class TrainingEventRepository:RepositoryGeneric<TrainingEventModel>
    {
        private readonly LearnNowDB db;
        public TrainingEventRepository(LearnNowDB ldb):base(ldb) {

            db = ldb;
            
        }
        public override async Task<TrainingEventModel> GetById(int id)
        {
            var te=await db.TrainingEvents
                .Include(te=>te.Course)
                .Include(te => te.Location)
                .Include(te => te.Teacher)
                .FirstOrDefaultAsync(te=>te.Id==id);
            return te!;

        }
        
        public override async Task<ICollection<TrainingEventModel>> GetAll()
        {
            return await db.TrainingEvents
                .Include(te=>te.Course)
                .Include(te=>te.Location)
                .Include (te=>te.Teacher)
                .ToArrayAsync();
        }

        public override async Task<int> Create(TrainingEventModel trainingEventModel)
        {
            if(await db.Courses.AnyAsync(c=>c.Id==trainingEventModel.CourseId) &&
                    await db.Locations.AnyAsync(l=>l.Id==trainingEventModel.LocationId) &&
                    await db.Teachers.AnyAsync(t=>t.Id==trainingEventModel.TeacherId)
            )
            {
                await base.Create(trainingEventModel);
                return trainingEventModel.Id;
            }
            throw new Exception("Una de las entidades enlazadas no existe");
        }

        public override async Task Update(TrainingEventModel trainingEventModel)
        {
            if(await db.Courses.AnyAsync(c=>c.Id ==trainingEventModel.Id) &&
                await db.Locations.AnyAsync(l=>l.Id==trainingEventModel.LocationId) &&
                await db.Teachers.AnyAsync(t=>t.Id ==trainingEventModel.TeacherId))
            {
                await base.Update(trainingEventModel);
            }
            throw new ArgumentException("Una de las entidades enlazadas no existe");
        }
    }
}
