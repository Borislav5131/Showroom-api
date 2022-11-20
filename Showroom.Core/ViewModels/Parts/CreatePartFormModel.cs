using System.ComponentModel.DataAnnotations;

namespace Showroom.Core.ViewModels.Parts
{
    public class CreatePartFormModel
    {
        [Required]
        public string Name { get; set; }
    }
}
