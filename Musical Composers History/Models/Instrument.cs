using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Composers_History.Models
{
    public class Instrument
    {
        public Instrument()
        {
            Pieces = new List<Piece>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public virtual ICollection<Piece> Pieces { get; set; }
    }
}
