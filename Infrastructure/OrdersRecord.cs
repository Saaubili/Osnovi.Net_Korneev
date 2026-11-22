namespace Infrastructure
{
	public record OrdersRecord(long ClientId, long OrderId, string ItemName, int ItemQuantity, float ItemPrice);
}
