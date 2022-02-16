using Common.Dto.Report.Report;
using Common.Dto.Shared;
using Common.Helpers.Pagination;
using Report.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Report.Test.TestDataHelper;

namespace Report.Test
{
    public class ReportControllerTest
    {
        private readonly ReportController _reportController;

        public ReportControllerTest()
        {
            _reportController = ConfigHelper.GetReportController();
        }

        [Fact]
        public async Task ReportController_ActionExecutes_ReturnsViewForList()
        {
            //Arrange
            var filter = new ReportFilter();

            //Act
            var result = await _reportController.GetReports(filter, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.IsType<GenericResult>(result);
            Assert.IsType<PagedResult<ReportDto>>(result.Data);
            Assert.True(result.Success);
        }


        [Theory]
        [ClassData(typeof(InsertReportClassData))]
        public async Task ReportController_ActionExecutes_ReturnsViewForCreateReport(InsertReportRequestDto requestDto)
        {
            //Arrange

            //Act
            var result = await _reportController.CreateReport(requestDto, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.IsType<GenericResult>(result);
            Assert.IsType<InsertReportResponseDto>(result.Data);
            Assert.True(result.Success);
        }
    }
}
