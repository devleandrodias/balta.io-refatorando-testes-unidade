using System;
using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeDiscountRepository : IDiscountRepository
    {
        public Discount Get(string code)
        {
            if (code == "12345678911")
                return new Discount(10, DateTime.Now.AddDays(5));

            if (code == "111111")
                return new Discount(10, DateTime.Now.AddDays(-5));

            return null;
        }
    }
}