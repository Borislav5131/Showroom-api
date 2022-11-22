using System.ComponentModel.DataAnnotations;

namespace Showroom.Core.ViewModels.Showrooms
{
    public class CreateShowroomFormModel
    {
        [Required]
        public string Name { get; set; }
    }
}
