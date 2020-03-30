using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlersTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFreeRepository _deliveryFreeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandlersTests()
        {
            _customerRepository = new FakeCustomerRepository();
            _deliveryFreeRepository = new FakeDeliveryFreeRepository();
            _discountRepository = new FakeDiscountRepository();
            _productRepository = new FakeProductRepository();
            _orderRepository = new FakeOrderRepository();
        }

        [TestMethod, TestCategory("Handlers")]
        public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
        {
            // TODO: Implementar
            Assert.IsTrue(true);
        }

        [TestMethod, TestCategory("Handlers")]
        public void Dado_um_cep_invalido_o_pedido_nao_deve_ser_gerado()
        {
            // TODO: Implementar
            Assert.IsTrue(true);
        }

        [TestMethod, TestCategory("Handlers")]
        public void Dado_um_promocode_inexistente_o_pedido_deve_ser_gerado_normalmente()
        {
            // TODO: Implementar
            Assert.IsTrue(true);
        }

        [TestMethod, TestCategory("Handlers")]
        public void Dado_um_pedido_sem_itens_o_mesmo_nao_pode_ser_gerado()
        {
            // TODO: Implementar
            Assert.IsTrue(true);
        }

        [TestMethod, TestCategory("Handlers")]
        public void Dado_um_comando_invalido_o_pedido_nao_pode_ser_gerado()
        {
            // TODO: Implementar
            Assert.IsTrue(true);
        }

        [TestMethod, TestCategory("Handlers")]
        public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();

            command.Customer = "12345678911";
            command.ZipCode = "123456";
            command.PromoCode = "12345678911";

            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(_customerRepository,
                _deliveryFreeRepository,
                _discountRepository,
                _productRepository,
                _orderRepository);

            handler.Handle(command);

            Assert.AreEqual(handler.Valid, true);
        }
    }
}