using System;
using MongoDB.Bson;
using System.Collections.Generic;

namespace DBeind.MongoDB
{
    class User
    {
        public ObjectId id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int failedLoginAttempts { get; set; }
        public int emailsActivated { get; set; }
        public DateTime created_at { get; set; }

        public List<Billing> billings { get; set; }
        public List<PasswordForgetToken> passwordForgetTokens { get; set; }
        public List<Userprofile> userprofiles { get; set; }
    }
}
