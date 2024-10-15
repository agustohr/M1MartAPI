using M1MartAPI.Carts.CartDtos;
using M1MartBusiness.Interfaces;
using M1MartDataAccess.Models;

namespace M1MartAPI.Carts
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public List<CartDto> GetAllCarts()
        {
            var carts = _cartRepository.GetAll().Select(c => new CartDto()
            {
                Id = c.Id,
                ProductName = c.Product.Name,
                BuyerUsername = c.BuyerUsername,
                Quantity = c.Quantity,
            });
            return carts.ToList();
        }

        public CartDto GetCartById(int id)
        {
            try
            {
                var cart = _cartRepository.GetById(id);
                return new CartDto()
                {
                    Id = cart.Id,
                    ProductName = cart.Product.Name,
                    BuyerUsername = cart.BuyerUsername,
                    Quantity = cart.Quantity,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CartUserDto> GetCartsByUsername(string username)
        {
            try
            {
                var carts = _cartRepository.GetByUsername(username).Select(c => new CartUserDto()
                {
                    Id= c.Id,
                    Product = new CartProductDto()
                    {
                        ProductId = c.ProductId,
                        ProductName = c.Product.Name,
                        Price = c.Product.Price
                    },
                    Quantity = c.Quantity,
                });
                return carts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CartDto CreateCart(CartUpsertDto dto)
        {
            try
            {
                Cart? cart = _cartRepository.GetByProductIdAndUsername(dto.ProductId, dto.BuyerUsername);
                if (cart == null)
                {
                    var newCart = new Cart()
                    {
                        ProductId = dto.ProductId,
                        BuyerUsername = dto.BuyerUsername,
                        Quantity = dto.Quantity,
                    };
                    var createdCart = _cartRepository.Add(newCart);
                    return new CartDto()
                    {
                        Id = createdCart.Id,
                        ProductName = createdCart.Product.Name,
                        BuyerUsername= createdCart.BuyerUsername,
                        Quantity = createdCart.Quantity,
                    };
                }
                else
                {
                    cart.Quantity += dto.Quantity;
                    var updatedCart = _cartRepository.Update(cart);
                    return new CartDto()
                    {
                        Id = updatedCart.Id,
                        ProductName = updatedCart.Product.Name,
                        BuyerUsername = updatedCart.BuyerUsername,
                        Quantity = updatedCart.Quantity,
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteCart(int id)
        {
            try
            {
                return _cartRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteCartByUsername(string username)
        {
            try
            {
                return _cartRepository.DeleteByUsername(username);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
