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
        var records = _ordersReader.GetItemsByClientId(clientId);

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

        string favoriteName = string.Empty;
        int maxQuantity = 0;
        foreach (var pair in quantityByName)
        {
            if (pair.Value > maxQuantity)
            {
                maxQuantity = pair.Value;
                favoriteName = pair.Key;
            }
        }

		return new ClientReportRow(clientId, ordersSum, favoriteName);
	}

    private OrderItem MapToOrderItem(OrdersRecord record)
    {
		return new OrderItem(
			record.ClientId,
			record.OrderId,
			record.ItemName,
			record.ItemQuantity,
			record.ItemPrice);
	}
}