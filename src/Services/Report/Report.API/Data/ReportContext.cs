using MongoDB.Driver;
using Report.API.Configuration;
using Report.API.Data.Interfaces;
using Report.API.Entities;

namespace Report.API.Data
{
    public class ReportContext : IReportContext
    {
        public ReportContext()
        {
            var client = new MongoClient(ReportAppConfiguration.GetMongoConnectionString());
            var database = client.GetDatabase(ReportAppConfiguration.GetMongoDatabaseName());

            Reports = database.GetCollection<Entities.Report>(nameof(Entities.Report));
            ReportTypePrms = database.GetCollection<ReportTypePrm>(nameof(ReportTypePrm));

            ReportTypePrms.InsertManyAsync(ReportContextSeed.GetPreconfiguredReportTypePrms());

            //ReportContextSeed.SeedData(ReportTypePrms);
        }

        public IMongoCollection<Entities.Report> Reports { get; }
        public IMongoCollection<ReportTypePrm> ReportTypePrms { get; }
    }
}
