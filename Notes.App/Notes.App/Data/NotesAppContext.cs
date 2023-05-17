using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.App.Models;

namespace Notes.App.Data
{
    public class NotesAppContext : DbContext
    {
        public NotesAppContext (DbContextOptions<NotesAppContext> options)
            : base(options)
        {
        }

        public DbSet<Notes.App.Models.Note> Note { get; set; } = default!;
    }
}
