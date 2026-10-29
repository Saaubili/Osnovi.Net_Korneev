using System.Globalization;
using Application;

namespace Presentation
{
    public class ConsoleReportPrinter : IReportPrinter
    {
        public void Print(List<ClientReportRow> rows)
        {
            Console.WriteLine("client_id orders_sum favorite_item_name");

            foreach (var row in rows)
            {
                string sum = row.OrdersSum.ToString("F2", CultureInfo.InvariantCulture);
                Console.WriteLine($"{row.ClientId} {sum} {row.FavoriteItemName}");
            }
        }
    }
}