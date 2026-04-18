using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNow.Class.Models
{
    public class TeacherModel:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [EmailAddress(ErrorMessage="Correo invalido")]
        public string Email { get; set; } = null!;
    }
}
