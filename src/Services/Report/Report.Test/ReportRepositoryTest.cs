using AutoMapper;
using Common.Dto.Report.Enums;
using Common.Dto.Report.Report;
using Common.Helpers.Pagination;
using Report.API.Repositories.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Report.Test.TestDataHelper;

namespace Report.Test
{
    public class ReportRepositoryTest
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportRepositoryTest()
        {
            _mapper = ConfigHelper.GetAutoMapperConfig();
            _reportRepository = ConfigHelper.GetReportRepository();
        }

        #region Report Tests
        [Theory]
        [ClassData(typeof(InsertReportClassData))]
        public async void InsertReport_InsertReportRequestDto_ReturnReportEntity(InsertReportRequestDto requestDto)
        {
            //Arrange
            var reportEntity = _mapper.Map<Report.API.Entities.Report>(requestDto);
            reportEntity.Id = TestDataHelper.GetTestReportId();

            //Act
            var result = await _reportRepository.InsertReportAsync(reportEntity, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Report.API.Entities.Report>(result);
            Assert.Equal(EnumReportTypeDto.ContactReport.ToString(), result.ReportTypeId.ToString());
        }

        [Theory]
        [ClassData(typeof(UpdateReportClassData))]
        public async void UpdateReport_InsertReportRequestDto_ReturnReportEntity(UpdateReportRequestDto requestDto)
        {
            //Arrange
            var reportEntity = _mapper.Map<Report.API.Entities.Report>(requestDto);

            //Act
            var result = await _reportRepository.UpdateReportAsync(reportEntity, CancellationToken.None);

            //Assert
            Assert.True(result);
        }

        [Fact]        
        public async void GetReport_Id_ReturnReport()
        {
            //Arrange
            string id = TestDataHelper.GetTestReportId();

            //Act
            var result = await _reportRepository.GetReportByIdAsync(id, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Report.API.Entities.Report>(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async void GetReports_Should_ReturnReportList()
        {
            //Arrange
            string id = TestDataHelper.GetTestReportId();
            var filter = new ReportFilter();

            //Act
            var result = await _reportRepository.GetReportsAsync(filter, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<PagedResult<Report.API.Entities.Report>>(result);
            Assert.True(result.Results.Count > 0);
            Assert.True(result.Results.Any(d => d.Id == id));
        }
        #endregion

        #region Report Types Tests
        [Fact]
        public async Task GetReportType_Should_ReturnReportType()
        {
            //Arrange
            var reportTypeId = Report.API.Enums.EnumReportType.ContactReport;

            //Act
            var result = await _reportRepository.GetReportTypeByIdAsync(reportTypeId, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Report.API.Entities.ReportTypePrm>(result);
            Assert.Equal(reportTypeId.ToString(), result.ReportId.ToString());
        }
        #endregion

    }
}
