using Showroom.Core.Interfaces;
using Showroom.Core.Repository;
using Showroom.Core.ViewModels.Cars;
using Showroom.Core.ViewModels.Showrooms;
using Showroom.Infrastructure.Data.Entities;

namespace Showroom.Core.Services
{
    public class ShowroomService : IShowroomService
    {
        private readonly IRepository _repo;

        public ShowroomService(IRepository repo)
        {
            _repo = repo;
        }

        public List<ShowroomViewModel> GetAllShowrooms()
        {
            return _repo.All<Infrastructure.Data.Entities.Showroom>()
                .Select(s => new ShowroomViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .ToList();
        }

        public Infrastructure.Data.Entities.Showroom? GetShowroomById(Guid showroomId)
            => _repo.All<Infrastructure.Data.Entities.Showroom>()
                .FirstOrDefault(s => s.Id == showroomId);

        public (bool added, string error) Create(CreateShowroomFormModel model)
        {
            bool added = false;
            string error = null;

            if (_repo.All<Infrastructure.Data.Entities.Showroom>().Any(s => s.Name == model.Name))
            {
                return (added, error = "Showroom name is taken!");
            }

            Infrastructure.Data.Entities.Showroom s = new Infrastructure.Data.Entities.Showroom()
            {
                Name = model.Name,
            };

            try
            {
                _repo.Add<Infrastructure.Data.Entities.Showroom>(s);
                _repo.SaveChanges();
                added = true;
            }
            catch (Exception e)
            {
                error = "Couldn't add showroom!";
            }

            return (added, error);
        }

        public (bool isEdit, string error) Edit(EditShowroomModel model)
        {
            bool isEdit = false;
            string error = null;

            var showroom = GetShowroomById(model.Id);

            if (showroom == null)
            {
                return (isEdit, error = "Invalid operation");
            }

            showroom.Name = model.Name;

            try
            {
                _repo.SaveChanges();
                isEdit = true;
            }
            catch (Exception e)
            {
                error = "Invalid operation";
            }

            return (isEdit, error);
        }

        public bool Delete(Guid showroomId)
        {
            var showroom = GetShowroomById(showroomId);

            if (showroom == null)
            {
                return false;
            }

            try
            {
                showroom.Cars.Clear();
                _repo.Remove<Infrastructure.Data.Entities.Showroom>(showroom);
                _repo.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public string GetShowroomName(Guid showroomId)
        {
            var showroom = GetShowroomById(showroomId);

            if (showroom == null)
            {
                return null;
            }

            return showroom.Name;
        }

        public EditShowroomModel GetDetailsOfShowroom(Guid showroomId)
            => _repo.All<Infrastructure.Data.Entities.Showroom>()
                .Where(s => s.Id == showroomId)
                .Select(s => new EditShowroomModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                })
                .First();

        public CreateCarFormModel CarCreateFormModel(Guid showroomId)
            => new CreateCarFormModel()
            {
                ShowroomId = showroomId,
                ShowroomName = GetShowroomName(showroomId),
            };
    }
}
