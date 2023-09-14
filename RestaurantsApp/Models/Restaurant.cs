namespace RestaurantsApp.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Restaurant
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)] // Specify the maximum length for VARCHAR
        public string Name { get; set; }

        [Required]
        [MaxLength(255)] // Specify the maximum length for VARCHAR
        public string Address { get; set; }

        [Required]
        [MaxLength(255)] // Specify the maximum length for VARCHAR
        public string City { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();
    }

}
