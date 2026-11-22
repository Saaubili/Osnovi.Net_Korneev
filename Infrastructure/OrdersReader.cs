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
                if (line.Trim() == string.Empty)
                {
                    continue;
                }

                var separatedItem = line.Split(",");
                var lineClientId = long.Parse(separatedItem[0]);

                if (lineClientId != clientId)
                {
                    continue;
                }

                var record = new OrdersRecord(lineClientId,
                                                long.Parse(separatedItem[1]),
                                                separatedItem[2],
                                                int.Parse(separatedItem[3]),
                                                float.Parse(separatedItem[4], CultureInfo.InvariantCulture));
                result.Add(record);
            }
            return result;
        }
    }
}