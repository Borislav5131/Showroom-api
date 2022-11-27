using Showroom.Core.Interfaces;
using Showroom.Core.Repository;
using Showroom.Core.ViewModels.Cars;
using Showroom.Core.ViewModels.Garages;
using Showroom.Infrastructure.Data.Entities;

namespace Showroom.Core.Services
{
    public class GarageService : IGarageService
    {
        private readonly IRepository _repo;
        private readonly ICarService _carService;

        public GarageService(IRepository repo, ICarService carService)
        {
            _repo = repo;
            _carService = carService;
        }

        public Garage? GetGarageById(Guid garageId)
            => _repo.All<Garage>()
                .FirstOrDefault(g => g.Id == garageId);

        public GarageViewModel GetGarage(Guid userId)
            => _repo.All<Garage>()
                .Where(g => g.UserId == userId)
                .Select(g => new GarageViewModel()
                {
                    Cars = g.Cars.Select(c => new CarViewModel()
                        {
                            Id = c.Id,
                            Brand = c.Brand,
                            Model = c.Model,
                            Type = c.Type,
                            Year = c.Year,
                            Image = "data:image;base64," + Convert.ToBase64String(c.Image),
                        }).ToList()
                })
                .First();

        public bool AddCar(Guid garageId, Guid carId)
        {
            var garage = this.GetGarageById(garageId);

            if (garage == null)
            {
                return false;
            }

            var car = _carService.GetCarById(carId);

            if (car == null)
            {
                return false;
            }

            if (garage.Cars.Contains(car))
            {
                return false;
            }

            garage.Cars.Add(car);

            try
            {
                _repo.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool RemoveCar(Guid garageId, Guid carId)
        {
            var garage = this.GetGarageById(garageId);

            if (garage == null)
            {
                return false;
            }

            var car = _carService.GetCarById(carId);

            if (car == null)
            {
                return false;
            }

            garage.Cars.Remove(car);

            try
            {
                _repo.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
