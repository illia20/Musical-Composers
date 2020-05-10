using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Composers_History.Models
{
    public class ComposerGenres
    {
        public int Id { get; set; }
        public int ComposerId { get; set; }
        public int GenreId { get; set; }

        public virtual Composer Composer { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
