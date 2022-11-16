using Showroom.Core.Interfaces;
using Showroom.Core.Repository;
using Showroom.Core.ViewModels.Parts;
using Showroom.Infrastructure.Data.Entities;

namespace Showroom.Core.Services
{
    public class PartService : IPartService
    {
        private readonly IRepository _repo;

        public PartService(IRepository repo)
        {
            _repo = repo;
        }

        public List<CarPartViewModel> GetAllParts()
            => _repo.All<Part>()
                .Select(p => new CarPartViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToList();

        public Part GetPartById(Guid partId)
            => _repo.All<Part>()
                .FirstOrDefault(p => p.Id == partId);

        public (bool added, string error) Create(CreatePartFormModel model)
        {
            bool added = false;
            string error = null;

            if (_repo.All<Part>().Any(p => p.Name == model.Name))
            {
                return (added, error = "Part name is taken!");
            }

            Part p = new Part()
            {
                Name = model.Name,
            };

            try
            {
                _repo.Add<Part>(p);
                _repo.SaveChanges();
                added = true;
            }
            catch (Exception e)
            {
                error = "Couldn't add part!";
            }
            
            return (added, error);
        }

        public bool Delete(Guid partId)
        {
            var part = GetPartById(partId);

            if (part == null)
            {
                return false;
            }

            var carWhichHavePart = _repo.All<Car>()
                .Where(c => c.Parts.Contains(part))
                .ToList();

            foreach (var car in carWhichHavePart)
            {
                if (car.Parts.Contains(part))
                {
                    car.Parts.Remove(part);
                }
            }

            try
            {
                _repo.Remove<Part>(part);
                _repo.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
