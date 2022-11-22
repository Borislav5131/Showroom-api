using NToastNotify;
using Showroom.Core.Interfaces;
using Showroom.Core.ViewModels;
using Showroom.Core.ViewModels.Parts;
using Showroom.Filters;

namespace Showroom.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [AuthenticationFilter]
    public class PartsController : Controller
    {
        private readonly IPartService _partService;
        private readonly IToastNotification _toastNotification;

        public PartsController(IPartService partService, IToastNotification toastNotification)
        {
            _partService = partService;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public ActionResult All()
        {
            var parts = _partService.GetAllParts();

            return View(parts);
        }

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePartFormModel model)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("Something wrong!");
                return View(model);
            }

            var (added, error) = _partService.Create(model);

            if (!added)
            {
                ModelState.AddModelError("", error);
                return View();
            }

            _toastNotification.AddSuccessToastMessage("Successfully created part!");
            return Redirect("/Parts/All");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var model = _partService.GetDetailsOfPart(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPartModel model)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("Something wrong!");
                return View(model);
            }

            var (isEdit, error) = _partService.Edit(model);

            if (!isEdit)
            {
                return View("Error", new ErrorViewModel() { ErrorMessage = error });
            }

            _toastNotification.AddSuccessToastMessage("Successfully edit part!");
            return Redirect($"/Parts/Edit/{model.Id}");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var deleted = _partService.Delete(id);

            if (!deleted)
            {
                return View("Error", new ErrorViewModel() { ErrorMessage = "Part can't be deleted!" });
            }

            _toastNotification.AddSuccessToastMessage("Successfully delete part!");
            return Redirect("/Parts/All");
        }
    }
}
