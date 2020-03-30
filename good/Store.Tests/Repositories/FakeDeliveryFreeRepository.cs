using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeDeliveryFreeRepository : IDeliveryFreeRepository
    {
        public decimal Get(string zipCode)
        {
            if (zipCode == "123456")
                return 10;

            else
                return 15;
        }
    }
}