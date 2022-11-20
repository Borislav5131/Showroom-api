using System.ComponentModel.DataAnnotations;

namespace Showroom.Core.ViewModels.Cars
{
    public class CreateCarFormModel
    {
        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public Guid ShowroomId { get; set; }

        [Required]
        public string ShowroomName { get; set; }
    }
}
