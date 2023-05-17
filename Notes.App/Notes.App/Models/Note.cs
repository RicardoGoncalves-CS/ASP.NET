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
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
