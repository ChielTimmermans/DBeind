using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBeind
{
    public partial class User
    {
        public User()
        {
            this.billings = new List<Billing>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string email { get; set; }

        [MaxLength(72)]
        public string password { get; set; }
        
        public int failedLoginAttempts { get; set; }
        
        public int emailsActivated { get; set; }
        public DateTime created_at { get; set; }

        public virtual ICollection<Billing> billings { get; set; }
    }
}
