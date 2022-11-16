using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Showroom.Infrastructure.Data.Entities
{
    public class Car
    {
        public Car()
        {
            Id = Guid.NewGuid();
            Parts = new List<Part>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Type { get; set; }

        public Guid ShowroomId { get; set; }

        [ForeignKey(nameof(ShowroomId))]
        public virtual Showroom Showroom { get; set; }

        public virtual List<Part> Parts { get; set; }
    }
}
