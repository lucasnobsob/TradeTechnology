using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public sealed class City : Entity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("postalCode")]
        public string PostalCode { get; set; }
        public State State { get; set; }
    }
}
