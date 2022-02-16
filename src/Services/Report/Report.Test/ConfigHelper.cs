using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;
using Report.API.Configuration;
using Report.API.Controllers;
using Report.API.Data;
using Report.API.Mapper;
using Report.API.Repositories;

namespace Report.Test
{
    public static class ConfigHelper
    {
        public static IMapper GetAutoMapperConfig()
        {
            var mapperConfig = new MapperConfiguration(d =>
            {
                d.AddProfile<MappingProfile>();
            });
            return mapperConfig.CreateMapper();
        }

        public static ReportContext GetContactDbContext()
        {
            return new ReportContext();
        }

        public static ReportRepository GetReportRepository()
        {
            return new ReportRepository(GetContactDbContext());
        }

        public static ReportController GetReportController()
        {
            var reportRepository = GetReportRepository();
            var mapper = GetAutoMapperConfig();
            var _mockRabbit = new Mock<IPublishEndpoint>();
            var _mockLogger = new Mock<ILogger<ReportController>>();

            return new ReportController(_mockLogger.Object, mapper, _mockRabbit.Object, reportRepository);
        }

        public static void Dispose()
        {
            var client = new MongoClient(ReportAppConfiguration.GetMongoConnectionString());
            client.DropDatabase(ReportAppConfiguration.GetMongoDatabaseName());
        }
    }
}
