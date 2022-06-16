using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2084_MAINPROJECT.Models
{
    public class Recipe
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(40), MinLength(3, ErrorMessage ="Recipe name can't be shorter than 3 characters")]
        public string Name { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Please insert an appropriate amount of ingredients")]


        public string Ingredients { get; set; }
        [Required]
        [MinLength(30, ErrorMessage = "It is preferred that the cooking process is provided in detail")]

        public string Process { get; set; }

        [ForeignKey("Origin")]
        public int? OriginId { get; set; }

        public Origin? Origin { get; set; }
    }
}
