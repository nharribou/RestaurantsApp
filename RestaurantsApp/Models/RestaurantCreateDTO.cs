using System.ComponentModel.DataAnnotations;

namespace RestaurantsApp.Models
{
    public class RestaurantCreateDTO
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(255)]
        public string City { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public double Rating { get; set; }
    }

}
