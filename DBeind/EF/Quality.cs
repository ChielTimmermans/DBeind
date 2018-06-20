using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBeind
{
    public partial class Quality
    {
        public Quality()
        {
            this.currentWatchingMovies = new List<CurrentWatchingMovie>();
        }
        [Key]
        public int Id { get; set; }
        
        [MaxLength(255)]
        public string name { get; set; }

        public virtual ICollection<CurrentWatchingMovie> currentWatchingMovies { get; set; }

    }
}
