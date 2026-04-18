using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnNow.Class.Models
{
    public class TraineeModel:IEntity
    {  public int Id { get; set; }
       public string Name { get; set; } = null!;
       public string Email { get; set; } = null!;
    }
}
