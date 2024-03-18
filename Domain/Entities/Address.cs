using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public sealed class Address : Entity
    {
        [BsonElement("street")]
        public string Street { get; set; }
        [BsonElement("number")]
        public string Number { get; set; }
        public City City { get; set; }
    }
}
