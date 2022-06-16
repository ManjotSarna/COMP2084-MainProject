using System.ComponentModel.DataAnnotations;

namespace COMP2084_MAINPROJECT.Models
{
    public class Origin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
