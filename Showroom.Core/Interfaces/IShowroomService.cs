using Showroom.Core.ViewModels.Cars;
using Showroom.Core.ViewModels.Showrooms;

namespace Showroom.Core.Interfaces
{
    public interface IShowroomService
    {
        List<ShowroomViewModel> GetAllShowrooms();
        Infrastructure.Data.Entities.Showroom? GetShowroomById(Guid showroomId);
        (bool added, string error) Create(CreateShowroomFormModel model);
        (bool isEdit, string error) Edit(EditShowroomModel model);
        bool Delete(Guid showroomId);
        string GetShowroomName(Guid showroomId);
        EditShowroomModel GetDetailsOfShowroom(Guid showroomId);
        CreateCarFormModel CarCreateFormModel(Guid showroomId);
    }
}
