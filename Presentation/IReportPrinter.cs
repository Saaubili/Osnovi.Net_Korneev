using Application;

namespace Presentation
{
    public interface IReportPrinter
    {
        void Print(List<ClientReportRow> rows);
    }
}