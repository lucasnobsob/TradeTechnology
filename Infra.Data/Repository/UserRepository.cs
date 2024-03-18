using Domain.Entities;
using Domain.Ports;
using Infra.Data.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
            databaseSettings.Value.DefaultConnection);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DbName);

            _collection = mongoDatabase.GetCollection<User>(
                databaseSettings.Value.UsersCollectionName);
        }

        public async Task<User> Add(User entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<User>> Find(Expression<Func<User, bool>> predicate)
        {
            return await _collection.FindAsync(predicate).Result.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _collection.FindAsync(_ => true).Result.ToListAsync();
        }

        public async Task Remove(Expression<Func<User, bool>> predicate)
        {
            await _collection.DeleteOneAsync(predicate);
        }

        public async Task<User> Update(Expression<Func<User, bool>> predicate, User entity)
        {
            await _collection.ReplaceOneAsync(predicate, entity);
            return entity;
        }
    }
}
