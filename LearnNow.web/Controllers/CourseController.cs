using LearnNow.Class;
using LearnNow.Class.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LearnNow.web.Controllers
{
    public class CourseController : Controller
    {
        private readonly IRepositoryGeneric<CourseModel> cr;
     

        public CourseController(IRepositoryGeneric<CourseModel> cr)
        {
            this.cr = cr;
        }

        [HttpGet("count")]
        public int Count()
        {
            return cr.CountCourses();
        }
        // GET: CourseController
        public async Task<ActionResult> Index()
        {   
            var courses=await cr.GetAll();
            return View(courses);
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CourseModel course)
        {   
            var id=await cr.Create(course);
            return RedirectToAction(nameof(Index));
        }

        // GET: CourseController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var course = await cr.GetById(id);
            return View();
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>Edit(CourseModel course)
        {
            await cr.Update(course);
            return RedirectToAction("Index");
        }

        // GET: CourseController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var course =await cr.GetById(id);
            return View(course);
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(CourseModel course)
        {
            await cr.Delete(course.Id);
            return RedirectToAction("Index");
        }
    }
}
