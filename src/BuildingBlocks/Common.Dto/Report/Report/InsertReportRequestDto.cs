using Common.Dto.Report.Enums;

namespace Common.Dto.Report.Report
{
    public class InsertReportRequestDto
    {
        public EnumServiceTypeDto ServiceTypeId { get; set; }
        public EnumReportTypeDto ReportTypeId { get; set; }
    }
}
