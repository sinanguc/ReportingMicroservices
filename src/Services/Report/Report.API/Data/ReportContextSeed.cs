using MongoDB.Driver;
using Report.API.Entities;
using System.Collections.Generic;

namespace Report.API.Data
{
    public static class ReportContextSeed
    {
        public static async void SeedData(IMongoCollection<ReportTypePrm> reportTypeCollection)
        {
            bool existReportTypePrm = reportTypeCollection.Find(d => true).Any();
            if (existReportTypePrm)
            {
                await reportTypeCollection.InsertManyAsync(GetPreconfiguredReportTypePrms());
            }
        }

        public static IEnumerable<ReportTypePrm> GetPreconfiguredReportTypePrms()
        {
            return new List<ReportTypePrm>()
            {
                new ReportTypePrm()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    ReportId = Enums.EnumReportType.ContactReport,
                    ServiceId = Enums.EnumServiceType.ContactService,
                    ReportName = "Contact List Group By Location",
                    DestinationSavePath = "Assets\\Files"
                }
            };
        }
    }
}
