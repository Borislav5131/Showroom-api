using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Showroom.Core.Interfaces;
using Showroom.Core.ViewModels;
using Showroom.Core.ViewModels.Cars;
using Showroom.Filters;

namespace Showroom.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Showroom.Extensions;
    using Showroom.Infrastructure.Data.Entities;

    [AuthenticationFilter]
    public class CarsController : Controller
    {
        private readonly ICarService _carService;
        private readonly IShowroomService _showroomService;
        private readonly IToastNotification _toastNotification;
        private readonly IPartService _partService;

        public CarsController(ICarService carService, IShowroomService showroomService, IToastNotification toastNotification, IPartService partService)
        {
            _carService = carService;
            _showroomService = showroomService;
            _toastNotification = toastNotification;
            _partService = partService;
        }

        [HttpGet]
        public ActionResult All(Guid showroomId)
        {
            var cars = _carService.GetAllCars(showroomId);

            ViewData["ShowroomName"] = _showroomService.GetShowroomName(showroomId);
            ViewData["ShowroomId"] = showroomId;
            ViewData["GarageId"] = HttpContext.Session.GetObject<User>("loggedUser").GarageId;

            return View(cars);
        }

        [HttpGet]
        public ActionResult Create(Guid showroomId)
        {
            var model = _showroomService.CarCreateFormModel(showroomId);

            var availableParts = _partService.GetAllParts();
            ViewBag.Parts = new List<SelectListItem>();

            foreach (var part in availableParts)
            {
                ViewBag.Parts.Add(new SelectListItem() {Value = part.Id.ToString(), Text = part.Name });
            }

            if (model == null)
            {
                return View("Error", new ErrorViewModel() {ErrorMessage = "Something get wrong!"});
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCarFormModel car, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                if (image == null)
                {
                    _toastNotification.AddErrorToastMessage("Image is required!");
                }

                return View(car);
            }

            var convertedImage = ConvertImageToBytes(image);
            var (added, error) = _carService.Create(car, convertedImage);

            if (!added)
            {
                ModelState.AddModelError("", error);
                return View(car);
            }

            _toastNotification.AddSuccessToastMessage("Successfully created car!");
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
                _toastNotification.AddErrorToastMessage("Something wrong!");
                return View(car);
            }

            var (isEdit, error) = _carService.Edit(car);

            if (!isEdit)
            {
                return View("Error", new ErrorViewModel() { ErrorMessage = error });
            }

            _toastNotification.AddSuccessToastMessage("Successfully edit car!");
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

            _toastNotification.AddSuccessToastMessage("Successfully deleted car!");
            return Redirect("/Showrooms/All");
        }

        [HttpGet]
        public ActionResult CarParts(Guid id)
        {
            var carParts = _carService.GetCarParts(id);

            return View(carParts);
        }

        private byte[] ConvertImageToBytes(IFormFile image)
        {
            var ms = new MemoryStream();
            image.CopyTo(ms);

            return ms.ToArray();
        }
    }
}
