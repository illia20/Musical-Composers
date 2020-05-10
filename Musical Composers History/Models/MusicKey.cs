using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Composers_History.Models
{
    public class MusicKey
    {
        public MusicKey()
        {
            Pieces = new List<Piece>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Piece> Pieces { get; set; } 
    }
}
