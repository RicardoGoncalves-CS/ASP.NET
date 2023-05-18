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
        [Display(Name = "File Name")]
        public string Title { get; set; }


        [Required]
        public string Content { get; set; }

        [Display(Name = "Date Created")]
        public DateTime CreationDate { get; set; }

        public bool IsSelected { get; set; }
    }
}
