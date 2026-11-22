namespace Domain
{
	public record OrderItem(long ClientId, long OrderId, string ItemName, int ItemQuantity, float ItemPrice);
}
