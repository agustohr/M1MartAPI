namespace M1MartAPI.Dashboard.DashboardDtos
{
    public class DashboardDto
    {
        public int UserTotal { get; set; }
        public int CategoryTotal { get; set; }
        public int ProductTotal { get; set; }
        public int TransactionTotal { get; set; }
        public decimal IncomeTotal { get; set; }

        public List<MonthlyTrendDto> MonthlySalesTrend { get; set; } = null!;
    }
}
