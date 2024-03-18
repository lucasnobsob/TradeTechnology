using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public sealed class Phone : Entity
    {
        [BsonElement("number")]
        public string Number { get; set; }
    }
}
