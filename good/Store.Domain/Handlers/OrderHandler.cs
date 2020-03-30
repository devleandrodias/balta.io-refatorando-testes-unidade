using System.Linq;
using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Contracts;
using Store.Domain.Entities;
using Store.Domain.Handlers.Contracts;
using Store.Domain.Repositories;
using Store.Domain.Utils;

namespace Store.Domain.Handlers
{
    public class OrderHandler : Notifiable, IHandler<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFreeRepository _deliveryFreeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandler(ICustomerRepository customerRepository,
            IDeliveryFreeRepository deliveryFreeRepository,
            IDiscountRepository discountRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _deliveryFreeRepository = deliveryFreeRepository;
            _discountRepository = discountRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public ICommandResult Handle(CreateOrderCommand command)
        {
            // 0. Fail, Fast, Validation

            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Pedido Inválido", command.Notifications);

            // 1. Recuperar o cliente
            var customer = _customerRepository.Get(command.Customer);

            // 2. Calcular a taxa de entrega
            var deliveryFree = _deliveryFreeRepository.Get(command.ZipCode);

            // 3. Obtém cupom de desconto
            var discount = _discountRepository.Get(command.PromoCode);

            // 4. Gerar o pedido
            var products = _productRepository.Get(ExtractGuids.Extract(command.Items)).ToList();

            var order = new Order(customer, deliveryFree, discount);

            foreach (var item in command.Items)
            {
                var product = products.Where(x => x.Id == item.Product).FirstOrDefault();
                order.AddItem(product, item.Quantity);
            }

            // 5. Agrupar as notificações
            AddNotifications(order.Notifications);

            // 6. Verifica se deu tudo certo 
            if (Invalid)
                return new GenericCommandResult(false, "Falha oa gerar pedido", Notifications);

            // 7. Retorna o resultado
            _orderRepository.Save(order);
            return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso", order);
        }
    }
}