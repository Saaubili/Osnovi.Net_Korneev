using System.Globalization;
using Application;

namespace Presentation
{
    public class FileReportPrinter : IReportPrinter
    {
        public void Print(List<ClientReportRow> rows)
        {
            var fileName = $"Orders_Report.txt";
            var path = Path.Combine(AppContext.BaseDirectory, fileName);

            var lines = new List<string> { "client_id orders_sum favorite_item_name" };

            foreach (var row in rows)
            {
                var sum = row.OrdersSum.ToString("F2", CultureInfo.InvariantCulture);
                lines.Add($"{row.ClientId} {sum} {row.FavoriteItemName}");
            }

            File.WriteAllLines(path, lines);
        }
    }
}