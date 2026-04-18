using LearnNow.Class;
using LearnNow.Class.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnNow.web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IRepositoryGeneric<TeacherModel> teacherRG;

        public TeacherController(IRepositoryGeneric<TeacherModel> teacherRG)
        {
            this.teacherRG = teacherRG;
        }
        // GET: TeacherController
        public async Task<ActionResult> Index()
        {
            var teachers = await this.teacherRG.GetAll();
            return View(teachers);
        }

        // GET: TeacherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TeacherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeacherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TeacherModel teacher)
        {
            if (teacher == null)
            {
                throw new ArgumentNullException(nameof(teacher));
            }
            await teacherRG.Create(teacher);
            return RedirectToAction("Index");

        }

        // GET: TeacherController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var teacher = await teacherRG.GetById(id);
            return View(teacher);
        }

        // POST: TeacherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TeacherModel teacher)
        {
            await teacherRG.Update(teacher);
            return RedirectToAction("Index");
        }

        // GET: TeacherController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var teacher = await teacherRG.GetById(id);
            return View(teacher);
        }

        // POST: TeacherController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(TeacherModel teacher)
        {
            await teacherRG.Delete(teacher.Id);
            return RedirectToAction("Index");
        }
    }
}
