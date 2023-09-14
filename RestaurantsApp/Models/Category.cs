using System.ComponentModel.DataAnnotations;

namespace RestaurantsApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)] // Specify the maximum length for VARCHAR
        public string Name { get; set; }
    }

}
