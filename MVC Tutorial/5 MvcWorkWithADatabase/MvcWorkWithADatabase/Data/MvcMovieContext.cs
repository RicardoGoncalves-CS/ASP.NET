using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcWorkWithADatabase.Models;

namespace MvcWorkWithADatabase.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<MvcWorkWithADatabase.Models.Movie> Movie { get; set; } = default!;
    }
}
