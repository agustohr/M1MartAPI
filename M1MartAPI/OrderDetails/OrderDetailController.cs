using M1MartAPI.OrderDetails.OrderDetailDtos;
using M1MartAPI.Shared;
using M1MartDataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace M1MartAPI.OrderDetails
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderDetailService _orderDetailService;
        public OrderDetailController(OrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet("{invoiceNumber}")]
        public IActionResult GetByInvoiceNumber(string invoiceNumber)
        {
            try
            {
                var orderdDetails = _orderDetailService.GetOrderDetailByIN(invoiceNumber);
                return Ok(new ResponseDto<List<OrderDetailDto>>() { 
                    Status = "SUCCESS",
                    Message = $"You've received {orderdDetails.Count()} order details.",
                    Data = orderdDetails
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
