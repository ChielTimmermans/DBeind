using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBeind
{
    public partial class Language
    {
        public Language()
        {
            this.currentWatchingMovies = new List<CurrentWatchingMovie>();
        }
        
        [Key]
        public int Id { get; set; }

        [MaxLength(45)]
        public string language { get; set; }

        public virtual ICollection<CurrentWatchingMovie> currentWatchingMovies { get; set; }
    }
}
