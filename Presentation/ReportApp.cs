using Application;

namespace Presentation
{
    public class ReportApp
    {
        private readonly IReportService _reportService;
        private readonly IReportPrinter _printer;

        public ReportApp(IReportService reportService, IReportPrinter printer)
        {
            _reportService = reportService;
            _printer = printer;
        }

        public void Run(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Укажите id клиента (или несколько id через пробел)");
                return;
            }

            var rows = new List<ClientReportRow>();

            foreach (var arg in args)
            {
                if (!long.TryParse(arg, out long clientId))
                {
                    Console.WriteLine($"'{arg}' не является числом");
                    return;
                }

                try
                {
                    rows.Add(_reportService.GetReport(clientId));
                }
                catch (ClientNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            _printer.Print(rows);
        }
    }
}