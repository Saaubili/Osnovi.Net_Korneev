namespace Application
{

    public interface IReportService
    {
        ClientReportRow GetReport(long clientId);
    }
}