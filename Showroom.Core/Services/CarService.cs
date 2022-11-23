using Microsoft.EntityFrameworkCore;
using Showroom.Core.Interfaces;
using Showroom.Core.Repository;
using Showroom.Core.ViewModels.Cars;
using Showroom.Core.ViewModels.Parts;
using Showroom.Infrastructure.Data.Entities;

namespace Showroom.Core.Services
{
    public class CarService : ICarService
    {
        private readonly IRepository _repo;
        private readonly IShowroomService _showroomService;
        private readonly IPartService _partService;

        public CarService(IRepository repo, IShowroomService showroomService, IPartService partService)
        {
            _repo = repo;
            _showroomService = showroomService;
            _partService = partService;
        }

        public List<CarViewModel> GetAllCars(Guid showroomId)
        {
            return _repo.All<Car>()
                .Where(c => c.Showroom.Id == showroomId)
                .Select(c => new CarViewModel()
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Type = c.Type,
                    Year = c.Year,
                    Image = "data:image;base64," + Convert.ToBase64String(c.Image),
                })
                .ToList();
        }

        public Car? GetCarById(Guid carId)
            => _repo.All<Car>()
                .FirstOrDefault(c => c.Id == carId);

        public (bool added, string error) Create(CreateCarFormModel model, byte[] image)
        {
            bool added = false;
            string error = null;

            if (_repo.All<Car>().Any(c => c.Model == model.Model))
            {
                return (added, error = "Car Model exist!");
            }

            if (image == null)
            {
                return (added, error = "Car Image is not correct!");
            }

            var showroom = _showroomService.GetShowroomById(model.ShowroomId);

            if (showroom == null)
            {
                return (added, error = "Showroom is not valid!");
            }

            Car c = new Car()
            {
                Brand = model.Brand,
                Model = model.Model,
                Year = model.Year,
                Type = model.Type,
                ShowroomId = model.ShowroomId,
                Showroom = showroom,
                Image = image,
            };

            foreach (var modelPart in model.Parts)
            {
                if (modelPart != null)
                {
                    var part = _partService.GetPartById(Guid.Parse(modelPart));

                    if (part != null)
                    {
                        c.Parts.Add(part);
                    }
                }
            }

            showroom.Cars.Add(c);

            try
            {
                _repo.Add<Car>(c);
                _repo.SaveChanges();
                added = true;
            }
            catch (Exception e)
            {
                error = "Couldn't create car!";
            }

            return (added, error);
        }

        public (bool isEdit, string error) Edit(EditCarModel model)
        {
            bool isEdit = false;
            string error = null;

            var car = GetCarById(model.Id);
            var showroom = _showroomService.GetShowroomById(model.ShowroomId);

            if (car == null || showroom == null)
            {
                return (isEdit, error = "Invalid operation");
            }

            car.Brand = model.Brand;
            car.Model = model.Model;
            car.Year = model.Year;
            car.Type = model.Type;

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

        public bool Delete(Guid carId)
        {
            var car = GetCarById(carId);

            if (car == null)
            {
                return false;
            }

            var showroom = _showroomService.GetShowroomById(car.ShowroomId);

            if (showroom == null)
            {
                return false;
            }

            try
            {
                showroom.Cars.Remove(car);
                _repo.Remove<Car>(car);
                _repo.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public EditCarModel GetDetailsOfCar(Guid carId)
            => _repo.All<Car>()
                .Where(c => c.Id == carId)
                .Select(c => new EditCarModel()
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    ShowroomId = c.ShowroomId,
                    Type = c.Type,
                    Year = c.Year,
                })
                .First();

        public List<CarPartViewModel> GetCarParts(Guid carId)
            => _repo.All<Car>()
                .Where(c => c.Id == carId)
                .Select(c => c.Parts)
                .First()
                .Select(p => new CarPartViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToList();

    }
}
