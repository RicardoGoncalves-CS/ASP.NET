using System.ComponentModel.DataAnnotations;

namespace Notes.App.Models
{
    public class Note
    {
        public Note()
        {
            CreationDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }


        [Required]
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
