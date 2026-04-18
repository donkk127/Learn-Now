using LearnNow.Class;
using LearnNow.Class.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnNow.web.Controllers
{
    public class TraineeController : Controller
    {
        private readonly IRepositoryGeneric<TraineeModel> traineeRG;

        public TraineeController(IRepositoryGeneric<TraineeModel> traineeRG)
        {
            this.traineeRG = traineeRG;
        }
        // GET: TraineeController
        public async Task<ActionResult> Index()
        {
            var trainees = await this.traineeRG.GetAll();
            return View(traineeRG);
        }

        // GET: TraineeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TraineeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TraineeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TraineeModel trainee)
        {
            if (trainee == null)
            {
                throw new ArgumentNullException(nameof(trainee));
            }
            await traineeRG.Create(trainee);
            return RedirectToAction("Index");

        }

        // GET: TraineeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var trainee = await traineeRG.GetById(id);
            return View(trainee);
        }

        // POST: TraineeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TraineeModel trainee)
        {
            await traineeRG.Update(trainee);
            return RedirectToAction("Index");
        }

        // GET: TraineeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var trainee = await traineeRG.GetById(id);
            return View(trainee);
        }

        // POST: TraineeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(TraineeModel trainee)
        {
            await traineeRG.Delete(trainee.Id);
            return RedirectToAction("Index");
        }
    }
}
