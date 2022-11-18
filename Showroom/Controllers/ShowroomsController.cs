using Showroom.Filters;

namespace Showroom.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Showroom.Core.Interfaces;
    using Showroom.Core.ViewModels;
    using Showroom.Core.ViewModels.Showrooms;

    [AuthenticationFilter]
    public class ShowroomsController : Controller
    {
        private readonly IShowroomService _showroomService;

        public ShowroomsController(IShowroomService showroomService)
        {
            _showroomService = showroomService;
        }

        [HttpGet]
        public ActionResult All()
        {
            var showrooms = _showroomService.GetAllShowrooms();

            return View(showrooms);
        }

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateShowroomFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var (added, error) = _showroomService.Create(model);

            if (!added)
            {
                ModelState.AddModelError("",error);
                return View();
            }

            return Redirect("/Showrooms/All");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var model = _showroomService.GetDetailsOfShowroom(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditShowroomModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var (isEdit, error) = _showroomService.Edit(model);

            if (!isEdit)
            {
                return View("Error", new ErrorViewModel() {ErrorMessage = error});
            }

            return Redirect($"/Showrooms/Edit/{model.Id}");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var deleted = _showroomService.Delete(id);

            if (!deleted)
            {
                return View("Error", new ErrorViewModel() {ErrorMessage = "Showroom can't be deleted!"});
            }

            return Redirect("/Showrooms/All");
        }
    }
}
