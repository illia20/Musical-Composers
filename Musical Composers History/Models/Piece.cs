using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Composers_History.Models
{
    public class Piece
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MusicKeyId { get; set; }
        public int ComposerId { get; set; }
        public int GenreId { get; set; }
        public int InstrumentId { get; set; }

        public virtual MusicKey Key { get; set; }
        public virtual Composer Composer { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Instrument Instrument { get; set; }
        
    }
}
