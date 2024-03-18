using Domain.Entities;
using Domain.Ports;
using Infra.Data.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infra.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _collection;

        public CustomerRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
            databaseSettings.Value.DefaultConnection);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DbName);

            _collection = mongoDatabase.GetCollection<Customer>(
                databaseSettings.Value.CustomersCollectionName);
        }

        public async Task Add(Customer entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task AddAll(IEnumerable<Customer> entity)
        {
            await _collection.InsertManyAsync(entity);
        }

        public async Task<IEnumerable<Customer>> Find(Expression<Func<Customer, bool>> predicate)
        {
            var customersFound = await _collection.FindAsync(predicate);
            return await customersFound.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customersFound = await _collection.FindAsync(_ => true);
            return await customersFound.ToListAsync();
        }

        public async Task Remove(Expression<Func<Customer, bool>> predicate)
        {
            await _collection.DeleteOneAsync(predicate);
        }

        public async Task<Customer> Update(Expression<Func<Customer, bool>> predicate, Customer entity)
        {
            await _collection.ReplaceOneAsync(predicate, entity);
            return entity;
        }
    }
}
