using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBeind
{
    public partial class CurrentWatchingMovie
    {
        public int languageID { get; set; }

        public int qualityID { get; set; }

        public TimeSpan currentTime { get; set; }

        public DateTime Watched_at { get; set; }

        public int userprofileID { get; set; }

        public int itemID { get; set; }

        public virtual Language Language { get; set; }

        public virtual Quality Quality { get; set; }
        
        public virtual Userprofile Userprofile { get; set; }

        public virtual Item Item { get; set; }
    }
}
