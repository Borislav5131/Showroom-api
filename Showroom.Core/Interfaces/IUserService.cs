using Showroom.Core.ViewModels.Home;
using Showroom.Infrastructure.Data.Entities;

namespace Showroom.Core.Interfaces
{
    public interface IUserService
    {
        User? Login(LoginViewModel loginModel);
    }
}
