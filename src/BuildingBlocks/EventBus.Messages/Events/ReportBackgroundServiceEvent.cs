using Common.Dto.Report.Enums;
using System;

namespace EventBus.Messages.Events
{
    public class ReportBackgroundServiceEvent
    {
        public string Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public EnumServiceTypeDto ServiceTypeId { get; set; }
        public EnumReportTypeDto ReportTypeId { get; set; }
        public EnumReportStatusTypeDto Status { get; set; }
        public string FilePath { get; set; }
    }
}
