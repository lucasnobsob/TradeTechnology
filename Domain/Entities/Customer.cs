using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public sealed class Customer : Entity
    {
        public Customer()
        {
            Phones = new List<Phone>();
        }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("cpf")]
        public string CPF { get; set; }
        public Address Address { get; set; }
        public IEnumerable<Phone> Phones { get; set; }

        public void AddPhone(Phone phone)
        {
            Phones.Append(phone);
        }
    }
}
