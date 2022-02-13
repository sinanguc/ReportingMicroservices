using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Moq;
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

            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

            return new ReportContext(config);
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


            return new ReportController(mapper, _mockRabbit.Object, reportRepository);
        }
    }
}
