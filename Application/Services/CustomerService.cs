using Domain.Entities;
using Domain.Ports;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        public ICustomerRepository _repository { get; set; }

        public CustomerService(ICustomerRepository customerAdapter)
        {
            _repository = customerAdapter;
        }

        public async Task Add(Customer customer)
        {
            await _repository.Add(customer);
        }

        public async Task<IEnumerable<Customer>> AddCustomersFromFile(IFormFile csvFile)
        {
            var filePath = await WriteFile(csvFile);

            var customers = await ReadFile(filePath);

            await _repository.AddAll(customers);

            return customers;
        }


        private async Task<string> WriteFile(IFormFile csvFile)
        {
            string destinationFolder = Directory.GetCurrentDirectory();
            destinationFolder = Directory.GetParent(destinationFolder)!.FullName + "\\files\\";
            string fullPath = Path.Combine(destinationFolder, csvFile.FileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await csvFile.CopyToAsync(stream);
            }

            return fullPath;
        }

        private async Task<IEnumerable<Customer>> ReadFile(string filePath)
        {
            var records = new List<Customer>();
            using (var reader = new StreamReader(filePath))
            {
                await reader.ReadLineAsync();
                var line = await reader.ReadLineAsync();
                while (line != null)
                {
                    var fields = line.Split(';');
                    records.Add(GetRecord(fields));

                    line = await reader.ReadLineAsync();
                }
            }

            return records;
        }

        private Customer GetRecord(string[] fields)
        {
            var customer = new Customer()
            {
                Name = fields[0],
                CPF = fields[1],
                Address = new Address()
                {
                    Street = fields[2],
                    Number = fields[3],
                    City = new City()
                    {
                        Name = fields[4],
                        State = new State()
                        {
                            Name = fields[5]
                        }
                    }
                }
            };
            customer.AddPhone(new Phone() { Number = fields[6] });

            return customer;
        }
    }
}
