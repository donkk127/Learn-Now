using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnNow.web.Controllers;
using Moq;
using LearnNow.Class.Models;
using LearnNow.Class;

namespace LearnNow.Test
{
 
    public class CountCoursesTest
    {
        [Fact]
        public void CountCourses_WhenCalled_ReturnsCorrectCount()
        {
            var fakeCourses = new List<CourseModel>
            {
                new CourseModel {Id=1, Title="Course 1" },
                new CourseModel {Id=2, Title="Course 2" },
                new CourseModel {Id=3, Title="Course 3" }

            };

            var mockRepo = new Mock<IRepositoryGeneric<CourseModel>>();
            
            mockRepo.Setup(repo=>repo.CountCourses()).Returns(fakeCourses.Count());
            var controller=new CourseController(mockRepo.Object);

            //Act
            int result = controller.Count();

            Assert.Equal(3, result);
        }    
    }
}
