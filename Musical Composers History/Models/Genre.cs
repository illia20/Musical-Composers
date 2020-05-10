using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Composers_History.Models
{
    public class Genre
    {
        public Genre()
        {
            Pieces = new List<Piece>();
            ComposerGenres = new List<ComposerGenres>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public virtual ICollection<ComposerGenres> ComposerGenres { get; set; }
        public virtual ICollection<Piece> Pieces { get; set; }
    }
}
