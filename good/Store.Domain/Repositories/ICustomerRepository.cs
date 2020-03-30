using Store.Domain.Entities;

namespace Store.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(string document);
    }
}