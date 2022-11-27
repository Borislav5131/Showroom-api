using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Showroom.Infrastructure.Data.Entities
{
    public class Garage
    {
        public Garage()
        {
            Id = Guid.NewGuid();
            Cars = new List<Car>();
        }

        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public virtual List<Car> Cars { get; set; }
    }
}
