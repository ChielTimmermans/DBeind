using System;
using MongoDB.Bson;

namespace DBeind.MongoDB
{
    class Userprofile
    {
        public ObjectId id { get; set; }
        public string name { get; set; }
        public string avatarUrl { get; set; }
        public DateTime dob { get; set; }
        public Boolean subtitlesOn { get; set; }
        public DateTime created_at { get; set; }
    }
}
