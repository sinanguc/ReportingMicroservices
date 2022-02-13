using Common.Dto.Report.Enums;
using System;

namespace Common.Dto.Report.Report
{
    public class UpdateReportRequestDto
    {
        public string Id { get; set; }
        public EnumServiceTypeDto ServiceTypeId { get; set; }
        public EnumReportTypeDto ReportTypeId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public EnumReportStatusTypeDto Status { get; set; }
        public string FilePath { get; set; }
    }
}
