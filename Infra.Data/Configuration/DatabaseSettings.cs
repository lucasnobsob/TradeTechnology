namespace Infra.Data.Configuration
{
    public class DatabaseSettings
    {
        public string DefaultConnection { get; set; }

        public string DbName { get; set; }

        public string CustomersCollectionName { get; set; }

        public string UsersCollectionName { get; set; }
    }
}
