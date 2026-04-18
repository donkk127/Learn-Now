using LearnNow.Class;
using LearnNow.Class.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnNow.web.Controllers
{
    public class LocationController : Controller
    {
        private readonly IRepositoryGeneric<LocationModel> locationRG;

        public LocationController(IRepositoryGeneric<LocationModel> locationRG)
        {
            this.locationRG = locationRG;
        }
        // GET: LocationController
        public async Task<ActionResult> Index()
        {
            var locations=await this.locationRG.GetAll();
            return View(locations);
        }

        // GET: LocationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LocationModel location)
        {
             if(location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }
             await locationRG.Create(location);
            return RedirectToAction("Index");

        }

        // GET: LocationController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var location=await locationRG.GetById(id);
            return View(location);
        }

        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LocationModel location)
        {
            await locationRG.Update(location);
            return RedirectToAction("Index");
        }

        // GET: LocationController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var location=await locationRG.GetById(id);
            return View(location);
        }

        // POST: LocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(LocationModel location)
        {
           await locationRG.Delete(location.Id);
            return RedirectToAction("Index");
        }
    }
}
