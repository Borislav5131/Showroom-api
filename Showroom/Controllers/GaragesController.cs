using NToastNotify;
using Showroom.Core.Interfaces;
using Showroom.Core.ViewModels;
using Showroom.Extensions;
using Showroom.Infrastructure.Data.Entities;

namespace Showroom.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class GaragesController : Controller
    {
        private readonly IGarageService _garageService;
        private readonly IToastNotification _toastNotification;

        public GaragesController(IGarageService garageService, IToastNotification toastNotification)
        {
            _garageService = garageService;
            _toastNotification = toastNotification;
        }

        public IActionResult AddCar(Guid garageId, Guid carId)
        {
            var added = _garageService.AddCar(garageId, carId);

            if (!added)
            {
                return View("Error", new ErrorViewModel() { ErrorMessage = "Can't add car to garage!" });
            }

            _toastNotification.AddSuccessToastMessage("Successfully add car to garage!");
            return Redirect("/");
        }

        public IActionResult RemoveCar(Guid garageId, Guid carId)
        {
            var removed = _garageService.RemoveCar(garageId, carId);

            if (!removed)
            {
                return View("Error", new ErrorViewModel() { ErrorMessage = "Can't remove car from garage!" });
            }

            _toastNotification.AddSuccessToastMessage("Successfully removed car from garage!");
            return Redirect("/");
        }
    }
}
