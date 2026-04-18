using LearnNow.Class;
using LearnNow.Class.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace LearnNow.web.Controllers
{
    public class PaymentService
    {

        public decimal CalculateChange(decimal courseCost, decimal amountPaid)
        {
            if (amountPaid < courseCost)
            {
                throw new ArgumentException("El monto pagado es insuficiente.");

                
            }
            return amountPaid - courseCost;
        }
    }
        

    public class PaymentRequest
    {
        public decimal CourseCost {  get; set; }

        public decimal amountPaid { get; set; }

    }
    public class TrainingEventController : Controller
    {

        private readonly TrainingEventRepository trainingEventRepository;
        private readonly IRepositoryGeneric<CourseModel> courseRG;
        private readonly IRepositoryGeneric<LocationModel> locationRG;
        private readonly IRepositoryGeneric<TeacherModel> teacherRG;

        public TrainingEventController (
            TrainingEventRepository ter,
            IRepositoryGeneric<CourseModel> c,
            IRepositoryGeneric<LocationModel> l,
            IRepositoryGeneric<TeacherModel> t
            )
        {
            trainingEventRepository = ter;
            courseRG = c;
            locationRG = l;
            teacherRG = t;
        }
        [Authorize(Roles ="Admin, Teacher, Trainee")]
        // GET: TrainingEventController

        public async Task<ActionResult> Index ()
        {
            var lstTE=await trainingEventRepository.GetAll();
            return View(lstTE.AsEnumerable());
        }

        // GET: TrainingEventController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        // GET: TrainingEventController/Create
        public async Task<ActionResult> Create(int? id)
        {
            ViewBag.Courses=new SelectList(await courseRG.GetAll(), "Id", "Title");
            ViewBag.Courses = new SelectList(await locationRG.GetAll(), "Id", "Name");
            ViewBag.Courses = new SelectList(await teacherRG.GetAll(), "Id", "Name");
            var model = new TrainingEventModel
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            };
            if (id.HasValue)
            {
                var course=await courseRG.GetById(id.Value);
                if (course == null) throw new ArgumentException("No existe el curso.");
                model.CourseId = course.Id;
                
            }
            return View(model);

        }

        [Authorize(Roles = "Admin")]
        // POST: TrainingEventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection formcollection)
        {
           var te=new TrainingEventModel();
            try
            {
                te.CourseId = int.Parse(formcollection["CourseId"]!);
                te.LocationId = int.Parse(formcollection["LocationId"]!);
                te.TeacherId = int.Parse(formcollection["TeacherId"]!);
                te.StartDate = DateTime.Parse(formcollection["StartDate"]!);
                te.EndDate = DateTime.Parse(formcollection["CourseId"]!);

                await trainingEventRepository.Create(te);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ViewBag.Courses = new SelectList(await courseRG.GetAll(), "Id", "Title");
                ViewBag.Locations=new SelectList(await locationRG.GetAll(), "Id", "Name");
                ViewBag.Teachers = new SelectList(await teacherRG.GetAll(), "Id", "Name");

                ModelState.AddModelError(string.Empty, ex.Message);

                return View(te);
            }
        }

        // GET: TrainingEventController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TrainingEventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TrainingEventController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var te=await trainingEventRepository.GetById(id);
            return View(te);
        }

        // POST: TrainingEventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(TrainingEventModel trainingEvent)
        {
            await trainingEventRepository.Delete(trainingEvent.Id);
            return RedirectToAction("Index");
        }
    }
}
