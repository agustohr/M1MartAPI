using M1MartAPI.Dashboard.DashboardDtos;
using M1MartBusiness.Interfaces;
using System.Globalization;

namespace M1MartAPI.Dashboard
{
    public class DashboardService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        public DashboardService(IUserRepository userRepository, ICategoryRepository categoryRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public DashboardDto GetData()
        {
            try
            {
                return new DashboardDto()
                {
                    UserTotal = _userRepository.CountUser(),
                    CategoryTotal = _categoryRepository.CountCategory(),
                    ProductTotal = _productRepository.CountProduct(),
                    TransactionTotal = _orderRepository.CountOrder(),
                    IncomeTotal = _orderRepository.GetTotalIncome(),
                    MonthlySalesTrend = GetSalesMonthByYear(DateTime.Now.Year),
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MonthlyTrendDto> GetSalesMonthByYear(int year)
        {
            var salesPerMonth = _orderRepository.GetMonthlySalesByYear(year).GroupBy(o => o.OrderDate.Month)
                .Select(g => new MonthlyTrendDto {
                    Year = year,
                    Month = g.Key,
                    MonthlySalesAmount = g.Sum(s => s.TotalProduct),
                    MonthlyTotalIncome = g.Sum(s => s.TotalPrice)
                });

            return salesPerMonth.ToList();
        }
    }
}
