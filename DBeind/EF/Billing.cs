using System;
using System.ComponentModel.DataAnnotations;

namespace DBeind
{
    public partial class Billing
    {

        [Key]
        public int Id { get; set; }
        
        public int price { get; set; }
        
        public DateTime date { get; set; }

        public DateTime paid { get; set; }

        public int UserID { get; set; }

        public int subscriptionID { get; set; }

        public virtual User User { get; set; }

        public virtual Subscription Subscription { get; set; }
    }
}
