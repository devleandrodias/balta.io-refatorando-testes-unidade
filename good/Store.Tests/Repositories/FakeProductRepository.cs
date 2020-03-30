using System;
using System.Collections.Generic;
using Store.Domain.Entities;

namespace Store.Tests.Repositories
{
    public class FakeProductRepository
    {
        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
            IList<Product> products = new List<Product>();

            products.Add(new Product("Produto 01", 1000, true));
            products.Add(new Product("Produto 02", 1000, true));
            products.Add(new Product("Produto 03", 1000, true));
            products.Add(new Product("Produto 04", 1000, false));
            products.Add(new Product("Produto 05", 1000, false));

            return products;
        }
    }
}