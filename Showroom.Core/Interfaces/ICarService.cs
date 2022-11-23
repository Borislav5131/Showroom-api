using Showroom.Core.ViewModels.Cars;
using Showroom.Core.ViewModels.Parts;
using Showroom.Infrastructure.Data.Entities;

namespace Showroom.Core.Interfaces
{
    public interface ICarService
    {
        List<CarViewModel> GetAllCars(Guid showroomId);
        Car? GetCarById(Guid carId);
        (bool added, string error) Create(CreateCarFormModel model, byte[] image);
        (bool isEdit, string error) Edit(EditCarModel model);
        bool Delete(Guid carId);
        EditCarModel GetDetailsOfCar(Guid carId);
        List<CarPartViewModel> GetCarParts(Guid carId);
    }
}
