using System;
using MongoDB.Bson;

namespace DBeind.MongoDB
{
    class PasswordForgetToken
    {
        public ObjectId id { get; set; }
        public string email { get; set; }
        public string hash { get; set; }
        public DateTime created_at { get; set; }
    }
}
