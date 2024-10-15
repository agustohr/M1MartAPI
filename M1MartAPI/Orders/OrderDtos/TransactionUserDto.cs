namespace M1MartAPI.Orders.OrderDtos
{
    public class TransactionUserDto
    {
        public string InvoiceNumber { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalProduct { get; set; }
        public List<TransactionUserDetailDto> TransactionDetails { get; set; } = null!;
    }
}
