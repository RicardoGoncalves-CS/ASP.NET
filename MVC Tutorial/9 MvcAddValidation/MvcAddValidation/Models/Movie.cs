using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MvcAddValidation.Models
{
    // Adding validation rules to the movie model
    // Added validation between is between ///

    // Validation attributes specify behaviour that we want to enforce on the model properties they're being applied to
    
    // [Required] and [MinimumLength] indicates that a property must have a value but nothing prevents the user from entering white space to satisfy this validation

    // [RegularExpression] attribute is used to limit what characters can be input.
    //
    // Genre:
    // Must only have letters;
    // The first letter is required to be uppercase. White spaces are allowed while numbers and special characters are not;
    //
    // Rating:
    // Require the first character to be an uppercase letter;
    // Allows special characters and numbers is subsequent spaces. "PG-13" is valid for a rating;

    // [Range] attribute constrains a value to within a specified range.

    // [StringLength] attribute lets to set the maximum length of a string property, and optionally its minimum length.

    // Value types (such as decimal, int, float, DateTime) are inherently required and don't need the [Required] attribute.

    // Validation rules helps make an app more robust. It also ensure that we can't forget to validate something and inadvertently let bad data into the database.
    // Form data isn't sent to the server until there are no client side validation errors.
    public class Movie
    {
        public int Id { get; set; }

        ///
        [StringLength(60, MinimumLength = 3)]
        ///
        public string Title { get; set; }

        [Display(Name = "Release Date"), DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        ///
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$") ,Required, StringLength(30)]
        ///
        public string Genre { get; set; }

        ///
        [Range(1, 100), DataType(DataType.Currency)]
        ///
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        ///
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(5)]
        ///
        public string Rating { get; set; }
    }
}
