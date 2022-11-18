using Showroom.Core.Interfaces;
using Showroom.Core.Repository;
using Showroom.Core.ViewModels.Home;
using Showroom.Infrastructure.Data.Entities;

namespace Showroom.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repo;

        public UserService(IRepository repo)
        {
            _repo = repo;
        }

        public User? Login(LoginViewModel loginModel)
            => _repo.All<User>()
                .FirstOrDefault(u => u.Username == loginModel.Username && u.Password == loginModel.Password);
    }
}
