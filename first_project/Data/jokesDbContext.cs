using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using first_project.Models;

    public class jokesDbContext : DbContext
    {
        public jokesDbContext (DbContextOptions<jokesDbContext> options)
            : base(options)
        {
        }

        public DbSet<first_project.Models.jokes> jokes { get; set; }
    }
