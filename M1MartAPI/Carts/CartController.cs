using M1MartAPI.Carts.CartDtos;
using M1MartAPI.Shared;
using Microsoft.AspNetCore.Mvc;

namespace M1MartAPI.Carts
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        public CartController(CartService cartService)
        {
            this._cartService = cartService;
        }

        [HttpGet]
        public IActionResult GetCarts()
        {
            try
            {
                var carts = _cartService.GetAllCarts();
                return Ok(new ResponseDto<List<CartDto>>()
                {
                    Status = "SUCCESS",
                    Message = $"You've received {carts.Count()} carts.",
                    Data = carts
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

        [HttpGet("{id}")]
        public IActionResult GetCart(int id)
        {
            try
            {
                var cart = _cartService.GetCartById(id);
                return Ok(new ResponseDto<CartDto>()
                {
                    Status = "SUCCESS",
                    Message  = $"Here you've a single cart requested for id {id}.",
                    Data = cart
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

        [HttpGet("user/{username}")]
        public IActionResult GetCartsByUsername(string username)
        {
            try {
                var carts = _cartService.GetCartsByUsername(username);
                return Ok(new ResponseDto<List<CartUserDto>>()
                {
                    Status = "SUCCESS",
                    Message = $"You've received {carts.Count()} carts.",
                    Data = carts
                });
            }
            catch (Exception ex) {
                return StatusCode(500, new ResponseDto<string>()
                {
                    Status = "SERVER ERROR",
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult AddCart([FromBody] CartUpsertDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createdCart = _cartService.CreateCart(dto);
                    return CreatedAtAction(nameof(GetCart), new { id = createdCart.Id }, new ResponseDto<CartDto>()
                    {
                        Status = "SUCCESS",
                        Message = $"Cart with id {createdCart.Id} is successfully added, here is the cart you sent.",
                        Data = createdCart
                    });
                }
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(new ResponseDto<string>()
                {
                    Status = "BAD REQUEST",
                    Message = "Invalid input data.",
                    Errors = errors
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

        [HttpDelete("{id}")]
        public IActionResult DeleteCart(int id)
        {
            try
            {
                var deleted = _cartService.DeleteCart(id);
                if (!deleted) return NotFound(new ResponseDto<string>()
                {
                    Status = "NOT FOUND",
                    Message = $"Cart with id {id} is not found."
                });
                return Ok(new ResponseDto<string>()
                {
                    Status = "SUCCESS",
                    Message = $"Cart with id {id} has been deleted."
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

        [HttpDelete("user/{username}")]
        public IActionResult DeleteCartByUsername(string username)
        {
            try
            {
                var deleted = _cartService.DeleteCartByUsername(username);
                if (!deleted) return NotFound(new ResponseDto<string>()
                {
                    Status = "NOT FOUND",
                    Message = $"Cart with username {username} is not found."
                });
                return Ok(new ResponseDto<string>()
                {
                    Status = "SUCCESS",
                    Message = $"Cart with username {username} has been deleted."
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
