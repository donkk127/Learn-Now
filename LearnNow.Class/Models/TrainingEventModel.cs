using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNow.Class.Models
{
    public class TrainingEventModel:IEntity
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CourseId { get; set; }
        public CourseModel Course { get; set; } = null!;
        public int LocationId { get; set; }
        public LocationModel Location { get; set; } = null!;
        public int TeacherId { get; set; }
        public TeacherModel Teacher { get; set; } = null!;
    }
}
