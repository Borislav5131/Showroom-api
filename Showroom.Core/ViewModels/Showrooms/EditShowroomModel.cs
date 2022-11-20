using System.ComponentModel.DataAnnotations;

namespace Showroom.Core.ViewModels.Showrooms
{
    public class EditShowroomModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
