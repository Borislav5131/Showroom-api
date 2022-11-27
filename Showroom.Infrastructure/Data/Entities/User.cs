using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Showroom.Infrastructure.Data.Entities
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            Garage = new Garage();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public Guid GarageId { get; set; }

        [NotMapped]
        [ForeignKey(nameof(GarageId))]
        public virtual Garage Garage { get; set; }
    }
}
