using M1MartAPI.OrderDetails.OrderDetailDtos;
using M1MartBusiness.Interfaces;

namespace M1MartAPI.OrderDetails
{
    public class OrderDetailService
    {
        private IOrderDetailRepository _orderDetailRepository;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public List<OrderDetailDto> GetOrderDetailByIN(string invoiceNumbe)
        {
            try
            {
                var orderDetails = _orderDetailRepository.GetByInvoiceNumber(invoiceNumbe).Select(od => new OrderDetailDto()
                {
                    ProductId = od.ProductId,
                    ProductName = od.Product.Name,
                    ProductPrice = od.Product.Price,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                });
                return orderDetails.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
