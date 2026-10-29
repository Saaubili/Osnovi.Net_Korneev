namespace Infrastructure
{
    public interface IOrdersReader
    {
        List<OrdersRecord> GetItemsByClientId(long clientId);
    }
}
