using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcAddSearch.Models
{
    public class MovieGenreViewModel
    {
        // This model contains:
        // A list of movies.
        // A SelectList containing the list of genres. This allows the user to select a genre form the list.
        // MovieGenre, which contains the selected genre.
        // SearchString, which contains the text users enter in the search text box.
        public List<Movie>? Movies { get; set; }
        public SelectList? Genres { get; set; }
        public string? MovieGenre { get; set; }
        public string? SearchString { get; set; }
    }
}
