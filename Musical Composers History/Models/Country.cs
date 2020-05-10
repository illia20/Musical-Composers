using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Composers_History.Models
{
    public class Country
    {
        public Country()
        {
            Composers = new List<Composer>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Composer> Composers { get; set; }
    }
}
