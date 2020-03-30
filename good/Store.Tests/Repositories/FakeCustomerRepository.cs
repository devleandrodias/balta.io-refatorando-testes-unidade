using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer GetCustomer(string document)
        {
            if (document == "12345678911")
                return new Customer("Bruce Wayne", "batman@gmail.com");

            return null;
        }
    }
}