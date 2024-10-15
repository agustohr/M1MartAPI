using M1MartAPI.Auth;
using M1MartAPI.Carts;
using M1MartAPI.Catalog;
using M1MartAPI.Categories;
using M1MartAPI.Dashboard;
using M1MartAPI.OrderDetails;
using M1MartAPI.Orders;
using M1MartAPI.Products;
using M1MartAPI.Users;
using M1MartBusiness.Interfaces;
using M1MartBusiness.Repositories;

namespace M1MartAPI.Configurations
{
    public static class ConfigureBusinessService
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<CategoryService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ProductService>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<CartService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<OrderService>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<OrderDetailService>();
            services.AddScoped<CatalogService>();
            services.AddScoped<DashboardService>();
            return services;
        }
    }
}
