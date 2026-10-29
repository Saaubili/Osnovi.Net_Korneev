using Domain;
using Infrastructure;

namespace Application;

public class ReportService : IReportService
{
    private readonly IOrdersReader _ordersReader;

    public ReportService(IOrdersReader ordersReader)
    {
        _ordersReader = ordersReader;
    }

    public ClientReportRow GetReport(long clientId)
    {
        List<OrdersRecord> records = _ordersReader.GetItemsByClientId(clientId);

        if (records.Count == 0)
        {
            throw new ClientNotFoundException($"Клиент с id {clientId} не найден");
        }

        var items = new List<OrderItem>();
        foreach (var record in records)
        {
            items.Add(MapToOrderItem(record));
        }

        float ordersSum = 0;
        var quantityByName = new Dictionary<string, int>();

        foreach (var item in items)
        {
            ordersSum += item.ItemQuantity * item.ItemPrice;

            if (quantityByName.ContainsKey(item.ItemName))
            {
                quantityByName[item.ItemName] += item.ItemQuantity;
            }
            else
            {
                quantityByName[item.ItemName] = item.ItemQuantity;
            }
        }

        string favoriteName = "";
        int maxQuantity = 0;
        foreach (var pair in quantityByName)
        {
            if (pair.Value > maxQuantity)
            {
                maxQuantity = pair.Value;
                favoriteName = pair.Key;
            }
        }

        var row = new ClientReportRow();
        row.ClientId = clientId;
        row.OrdersSum = ordersSum;
        row.FavoriteItemName = favoriteName;
        return row;
    }

    private OrderItem MapToOrderItem(OrdersRecord record)
    {
        var item = new OrderItem();
        item.ClientId = record.ClientId;
        item.OrderId = record.OrderId;
        item.ItemName = record.ItemName;
        item.ItemQuantity = record.ItemQuantity;
        item.ItemPrice = record.ItemPrice;
        return item;
    }
}