using System.ComponentModel.DataAnnotations;

namespace Showroom.Core.ViewModels.Parts
{
    public class EditPartModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
