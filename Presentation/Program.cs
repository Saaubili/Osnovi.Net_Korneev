using Application;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddApplication();
            services.AddSingleton<IReportPrinter, ConsoleReportPrinter>();
            services.AddSingleton<ReportApp>();

            var provider = services.BuildServiceProvider();
            var app = provider.GetRequiredService<ReportApp>();

            var inputedIds = GetIds();

            app.Run(inputedIds);
        }

        private static string[] GetIds()
        {
            Console.WriteLine("Введите id клиентов через пробел:");
            var line = Console.ReadLine();
            return line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}