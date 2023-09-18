namespace RestaurantsApp.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Restaurant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)] 
        public string Name { get; set; }
        [Required]
        [MaxLength(255)] 
        public string Address { get; set; }
        [Required]
        [MaxLength(255)] 
        public string City { get; set; }
        [ForeignKey("CategoryId")] 
        public int CategoryId { get; set; }

    }
}