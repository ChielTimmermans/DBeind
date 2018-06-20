using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBeind
{
    public partial class Subscription
    {

        [Key]
        public int Id { get; set; }
        
        public int price { get; set; }
        
        public string qualitiesID { get; set; }
        
    }
}
