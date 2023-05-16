using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcAddControllerMethodAndViews.Models
{
    public class Movie
    {
        // Adding DataAnnotations
        
        // The Display attribute specifies what to display for the name of a field (In this case "Release Date" instead of "ReleaseDate".
        // The DataType attribute specifies the type of the Data (Date), so the time information stored in the field isn't displayed.
        // The [Column(TypeName = "decimal(18 ,2)")] data annotation is required so Entity Framework Core can correctly map Price to currency in the database.

        public int Id { get; set; }
        public string? Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
