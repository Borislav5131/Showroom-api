using Showroom.Core.ViewModels.Garages;
using Showroom.Infrastructure.Data.Entities;

namespace Showroom.Core.Interfaces
{
    public interface IGarageService
    {
        Garage? GetGarageById(Guid garageId);
        GarageViewModel GetGarage(Guid userId);
        bool AddCar(Guid garageId, Guid carId);
        bool RemoveCar(Guid garageId, Guid carId);
    }
}
