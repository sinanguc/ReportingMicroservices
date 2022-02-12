using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Report.API.Data.Interfaces;
using Report.API.Entities;

namespace Report.API.Data
{
    public class ReportContext : IReportContext
    {
        public ReportContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Reports = database.GetCollection<Entities.Report>(nameof(Entities.Report));
            ReportTypePrms = database.GetCollection<ReportTypePrm>(nameof(ReportTypePrm));

            ReportTypePrms.InsertManyAsync(ReportContextSeed.GetPreconfiguredReportTypePrms());

            //ReportContextSeed.SeedData(ReportTypePrms);
        }

        public IMongoCollection<Entities.Report> Reports { get; }
        public IMongoCollection<ReportTypePrm> ReportTypePrms { get; }
    }
}
