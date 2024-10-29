namespace M1MartAPI.Dashboard.DashboardDtos
{
    public class MonthlyTrendDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int MonthlySalesAmount { get; set; }
        public decimal MonthlyTotalIncome { get; set; }
    }
}
