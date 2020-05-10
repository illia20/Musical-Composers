using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Composers_History.Models
{
    public class ComposersDbContext: DbContext
    {
        public ComposersDbContext(DbContextOptions<ComposersDbContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Piece> Pieces { get; set; }
        public virtual DbSet<Instrument> Instruments { get; set; }
        public virtual DbSet<Composer> Composers { get; set; }
        public virtual DbSet<ComposerGenres> ComposerGenres { get; set; }
        public virtual DbSet<MusicKey> MusicKeys { get; set; }

    }
}
