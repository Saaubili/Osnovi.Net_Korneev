using System.Globalization;

namespace Infrastructure
{
    public class OrdersReader : IOrdersReader
    {

        private readonly string FileName = "orders_log.csv";

        public List<OrdersRecord> GetItemsByClientId(long clientId)
        {
            var path = Path.Combine(AppContext.BaseDirectory, FileName);
            var allLines = File.ReadAllLines(path);

            var result = new List<OrdersRecord>();

            for (int i = 1; i < allLines.Length; i++)
            {
                var line = allLines[i];
                if (line.Trim() == "")
                {
                    continue;
                }

                var seperatedItem = line.Split(",");
                var lineClientId = long.Parse(seperatedItem[0]);

                if (lineClientId != clientId)
                {
                    continue;
                }

                var record = new OrdersRecord();
                record.ClientId = lineClientId;
                record.OrderId = long.Parse(seperatedItem[1]);
                record.ItemName = seperatedItem[2];
                record.ItemQuantity = int.Parse(seperatedItem[3]);
                record.ItemPrice = float.Parse(seperatedItem[4], CultureInfo.InvariantCulture);
                result.Add(record);

            }
            return result;
        }

    }
}
