using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public sealed class State : Entity
    {

        [BsonElement("name")]
        public string Name { get; set; }
    }
}
