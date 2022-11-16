using Showroom.Core.ViewModels.Parts;
using Showroom.Infrastructure.Data.Entities;

namespace Showroom.Core.Interfaces
{
    public interface IPartService
    {
        List<CarPartViewModel> GetAllParts();
        Part GetPartById(Guid partId);
        (bool added, string error) Create(CreatePartFormModel model);
        bool Delete(Guid partId);
    }
}
