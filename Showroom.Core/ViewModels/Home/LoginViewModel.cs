using System.ComponentModel.DataAnnotations;

namespace Showroom.Core.ViewModels.Home
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
