using System;
using MongoDB.Bson;

namespace DBeind.MongoDB
{
    class Billing
    {
        public ObjectId id { get; set; }
        public int price { get; set; }
        public DateTime TIMESTAMP { get; set; }
        public DateTime paid { get; set; }
    }
}
