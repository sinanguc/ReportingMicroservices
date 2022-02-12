using MongoDB.Driver;

namespace Report.API.Data.Interfaces
{
    public interface IReportContext
    {
        IMongoCollection<Report.API.Entities.Report> Reports { get; }
        IMongoCollection<Report.API.Entities.ReportTypePrm> ReportTypePrms { get; }
    }
}
