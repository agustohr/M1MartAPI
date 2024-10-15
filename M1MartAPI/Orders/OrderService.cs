using M1MartAPI.Orders.OrderDtos;
using M1MartBusiness.Interfaces;
using M1MartDataAccess.Models;

namespace M1MartAPI.Orders
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ICartRepository _cartRepository;
        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _cartRepository = cartRepository;
        }

        public List<OrderDto> GetAllOrders()
        {
            var orders = _orderRepository.GetAll().Select(o => new OrderDto() { 
                InvoiceNumber = o.InvoiceNumber,
                BuyerUsername = o.BuyerUsername,
                TotalProduct = o.TotalProduct,
                TotalPrice = o.TotalPrice,
                OrderDate = o.OrderDate,
            });
            return orders.ToList();
        }

        public OrderDto GetOrder(string invoiceNumber)
        {
            try {
                var order = _orderRepository.GetByIN(invoiceNumber);
                return new OrderDto()
                {
                    InvoiceNumber = order.InvoiceNumber,
                    BuyerUsername = order.BuyerUsername,
                    TotalProduct = order.TotalProduct,
                    TotalPrice = order.TotalPrice,
                    OrderDate = order.OrderDate,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<TransactionUserDto> GetOrdersByUsername(string username)
        {
            try
            {
                var orders = _orderRepository.GetByUsername(username).Select(o => new TransactionUserDto()
                {
                    InvoiceNumber = o.InvoiceNumber,
                    OrderDate = o.OrderDate,
                    TotalPrice = o.TotalPrice,
                    TotalProduct = o.TotalProduct,
                    TransactionDetails = o.OrderDetails.Select(od => new TransactionUserDetailDto()
                    {
                        ProductId = od.ProductId,
                        ProductName = od.Product.Name,
                        Quantity = od.Quantity,
                        UnitPrice = od.UnitPrice,
                    }).ToList()
                });
                return orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OrderDto CheckoutOrder(CreateOrderDto dto)
        {
            try
            {
                var order = new Order()
                {
                    InvoiceNumber = CreateInvoiceNumber(),
                    BuyerUsername = dto.BuyerUsername,
                    TotalProduct = dto.TotalProduct,
                    TotalPrice = dto.TotalPrice,
                    OrderDate = DateTime.Now,
                };
                var orderCreated = _orderRepository.Add(order);

                List<OrderDetail> orderDetails = new List<OrderDetail>();
                List<int> cartIds = new List<int>();
                foreach (var od in dto.OrderDetails)
                {
                    var orderDetail = new OrderDetail()
                    {
                        InvoiceNumber = orderCreated.InvoiceNumber,
                        ProductId = od.ProductId,
                        Quantity = od.Quantity,
                        UnitPrice = od.UnitPrice,
                    };
                    
                    orderDetails.Add(orderDetail);
                    cartIds.Add(od.CartId);
                }
                
                var orderDetailCreated = _orderDetailRepository.AddList(orderDetails);

                _cartRepository.DeleteList(cartIds);

                return new OrderDto()
                {
                    InvoiceNumber = orderCreated.InvoiceNumber,
                    BuyerUsername = orderCreated.BuyerUsername,
                    TotalProduct = orderCreated.TotalProduct,
                    TotalPrice = orderCreated.TotalPrice,
                    OrderDate = orderCreated.OrderDate,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string CreateInvoiceNumber()
        {
            string monthNow = DateTime.Now.Month.ToString("00");
            string yearNow = DateTime.Now.ToString("yy");
            string code = $"{monthNow}-{yearNow}";
            string increamentNumber = (_orderRepository.CountSpesificInvoiceNumber(code) + 1).ToString("0000");
            return $"{code}-{increamentNumber}";
        }
        
    }
}
