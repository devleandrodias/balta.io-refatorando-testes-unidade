using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;

namespace Store.Tests.Commands
{
    [TestClass]
    public class CreateOrderCommandTests
    {
        [TestMethod, TestCategory("Handlers")]
        public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            CreateOrderCommand command = new CreateOrderCommand();

            command.Customer = "";
            command.ZipCode = "13345432";
            command.PromoCode = "123456";

            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }
    }
}