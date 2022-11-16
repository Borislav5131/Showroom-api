using Showroom.Core.ViewModels.Cars;
using Showroom.Core.ViewModels.Parts;
using Showroom.Infrastructure.Data.Entities;

namespace Showroom.Core.Interfaces
{
    public interface IPartService
    {
        List<CarPartViewModel> GetAllParts();
        Part GetPartById(Guid partId);
        (bool added, string error) Create(CreatePartFormModel model);
        (bool isEdit, string error) Edit(EditPartModel model);
        bool Delete(Guid partId);
        EditPartModel GetDetailsOfPart(Guid partId);
    }
}
