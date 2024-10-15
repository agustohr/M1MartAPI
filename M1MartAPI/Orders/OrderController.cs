using M1MartAPI.Orders.OrderDtos;
using M1MartAPI.Shared;
using M1MartDataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace M1MartAPI.Orders
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            try {
                var orders = _orderService.GetAllOrders();
                return Ok(new ResponseDto<List<OrderDto>>()
                {
                    Status = "SUCCESS",
                    Message = $"You've received {orders.Count()} orders.",
                    Data = orders
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{invoiceNumber}")]
        public IActionResult GetOrder(string invoiceNumber)
        {
            try {
                var order = _orderService.GetOrder(invoiceNumber);
                return Ok(new ResponseDto<OrderDto>()
                {
                    Status = "SUCCESS",
                    Message = $"Here you've a single order requested for invoice number {invoiceNumber}.",
                    Data = order
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }

        [HttpGet("transaction-user/{username}")]
        public IActionResult GetTransactionUser(string username)
        {
            try
            {
                var transactions = _orderService.GetOrdersByUsername(username);
                return Ok(new ResponseDto<List<TransactionUserDto>>()
                {
                    Status = "SUCCESS",
                    Message = $"You've received {transactions.Count()} transactions.",
                    Data = transactions
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }

        [HttpPost("checkout")]
        public IActionResult Checkout([FromBody] CreateOrderDto dto)
        {
            try
            {
                var order = _orderService.CheckoutOrder(dto);
                return Ok(new ResponseDto<OrderDto>()
                {
                    Status = "Success",
                    Message = "Cart is successfully checkout",
                    Data = order
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }
    }
}
