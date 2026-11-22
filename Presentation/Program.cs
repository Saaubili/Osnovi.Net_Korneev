using Application;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0 || (args[0] != "console" && args[0] != "file"))
            {
                Console.WriteLine("Первым параметром укажите режим: console или file");
                return;
            }

            var mode = args[0];

            var services = new ServiceCollection();
            services.AddApplication();

            if (mode == "file")
            {
                services.AddSingleton<IReportPrinter, FileReportPrinter>();
            }
            else
            {
                services.AddSingleton<IReportPrinter, ConsoleReportPrinter>();
            }

            services.AddSingleton<ReportApp>();

            var provider = services.BuildServiceProvider();
            var app = provider.GetRequiredService<ReportApp>();

            var inputedIds = GetIds();

            app.Run(inputedIds);
        }

        private static long[] GetIds()
        {
            Console.WriteLine("Введите id клиентов через пробел или запятую:");
            var line = Console.ReadLine();

            if (ClientIdsParser.TryParse(line, out var ids))
            {
                return ids;
            }

            Console.WriteLine("Нужны только числа через пробел или запятую");
            return [];
        }
    }
}