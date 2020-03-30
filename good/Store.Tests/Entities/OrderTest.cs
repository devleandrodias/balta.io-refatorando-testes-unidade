using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderTest
    {
        // Red, Green, Refactor

        private readonly Customer _customer = new Customer("Leandro Dias", "leandro@teste.com");

        private readonly Product _product = new Product("MacBook Air 2019", 10, true);

        private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));

        [TestMethod, TestCategory("Domain")]
        public void Dados_um_novo_pedido_valido_ele_deve_gerar_um_numero_com_8_caracteres()
        {
            Order order = new Order(_customer, 0, null);
            Assert.AreEqual(8, order.Number.Length);
        }

        [TestMethod, TestCategory("Domain")]
        public void Dado_um_novo_pedido_seu_status_deve_ser_aguardando_pagamento()
        {
            Order order = new Order(_customer, 0, null);
            Assert.AreEqual(EOrderStatus.WaitingPayment, order.Status);
        }

        [TestMethod, TestCategory("Domain")]
        public void Dado_um_novo_pagamento_seu_status_deve_ser_aguardando_entrega()
        {
            Order order = new Order(_customer, 0, null);
            order.AddItem(_product, 1);
            order.Pay(10);
            Assert.AreEqual(EOrderStatus.WaitingDelivery, order.Status);
        }

        [TestMethod, TestCategory("Domain")]
        public void Dado_um_pedido_cancelado_seu_status_deve_ser_cancelado()
        {
            Order order = new Order(_customer, 0, null);
            order.Calcel();
            Assert.AreEqual(EOrderStatus.Canceled, order.Status);
        }

        [TestMethod, TestCategory("Domain")]
        public void Dado_um_novo_item_sem_o_produto_o_mesmo_nao_deve_ser_adicionado()
        {
            Order order = new Order(_customer, 0, null);
            order.AddItem(null, 10);
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod, TestCategory("Domain")]
        public void Dado_um_novo_item_com_quantidade_zero_ou_menor_o_mesmo_nao_deve_ser_adicionado()
        {
            Order order = new Order(_customer, 0, null);
            order.AddItem(_product, 0);
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod, TestCategory("Domain")]
        public void Dados_um_novo_pedido_valido_seu_total_deve_ser_50()
        {
            Order order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 50);
        }

        [TestMethod, TestCategory("Domain")]
        public void Dado_um_desconto_expirado_o_valor_do_pedido_deve_ser_60()
        {
            Discount discountExpired = new Discount(10, DateTime.Now.AddDays(-1));
            Order order = new Order(_customer, 10, discountExpired);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod, TestCategory("Domain")]
        public void Dado_um_desconto_invalido_o_valor_do_pedido_deve_ser_60()
        {
            Order order = new Order(_customer, 10, null);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod, TestCategory("Domain")]
        public void Dado_um_desconto_de_10_o_valor_do_pedido_deve_ser_50()
        {
            Order order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 50);
        }

        [TestMethod, TestCategory("Domain")]
        public void Dado_uma_taxa_de_entrega_de_10_o_valor_do_pedido_deve_ser_60()
        {
            Order order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 6);
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod, TestCategory("Domain")]
        public void Dado_uma_pedido_sem_cliente_o_mesmo_deve_ser_invalido()
        {
            Order order = new Order(null, 10, _discount);
            Assert.AreEqual(order.Valid, false);
        }
    }
}