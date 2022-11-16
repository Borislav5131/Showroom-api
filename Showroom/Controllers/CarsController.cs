using Showroom.Core.Interfaces;
using Showroom.Core.ViewModels;
using Showroom.Core.ViewModels.Cars;

namespace Showroom.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : Controller
    {
        private readonly ICarService _carService;
        private readonly IShowroomService _showroomService;

        public CarsController(ICarService carService, IShowroomService showroomService)
        {
            _carService = carService;
            _showroomService = showroomService;
        }

        [HttpGet]
        public ActionResult All(Guid showroomId)
        {
            var cars = _carService.GetAllCars(showroomId);

            ViewData["ShowroomName"] = _showroomService.GetShowroomName(showroomId);
            ViewData["ShowroomId"] = showroomId;

            return View(cars);
        }

        [HttpGet]
        public ActionResult Create(Guid showroomId)
        {
            var model = _showroomService.CarCreateFormModel(showroomId);

            if (model == null)
            {
                return View("Error", new ErrorViewModel() {ErrorMessage = "Something get wrong!"});
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCarFormModel car)
        {
            if (!ModelState.IsValid)
            {
                return View(car);
            }

            var (added, error) = _carService.Create(car);

            if (!added)
            {
                ModelState.AddModelError("", error);
                return View(car);
            }

            return Redirect($"/Cars/All?showroomId={car.ShowroomId}");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var model = _carService.GetDetailsOfCar(id);

            if (model == null)
            {
                return View("Error", new ErrorViewModel() { ErrorMessage = "Something get wrong!" });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCarModel car)
        {
            if (!ModelState.IsValid)
            {
                return View(car);
            }

            var (isEdit, error) = _carService.Edit(car);

            if (!isEdit)
            {
                return View("Error", new ErrorViewModel() { ErrorMessage = error });
            }

            return Redirect($"/Cars/Edit/{car.Id}");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var deleted = _carService.Delete(id);

            if (!deleted)
            {
                return View("Error", new ErrorViewModel() {ErrorMessage = "Car can't be deleted!"});
            }

            return Redirect("/Showrooms/All");
        }

        [HttpGet]
        public ActionResult CarParts(Guid id)
        {
            var carParts = _carService.GetCarParts(id);

            return View(carParts);
        }
    }
}
