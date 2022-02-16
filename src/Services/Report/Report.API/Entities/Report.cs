using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Report.API.Enums;

namespace Report.API.Entities
{
    public partial class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("RequestDate")]
        public BsonDateTime RequestDate { get; set; }

        [BsonElement("CompletedDate")]
        public BsonDateTime CompletedDate { get; set; }

        [BsonElement("ServiceTypeId")]
        public EnumServiceType ServiceTypeId { get; set; }

        [BsonElement("ReportTypeId")]
        public EnumReportType ReportTypeId { get; set; }

        [BsonElement("Status")]
        public EnumReportStatusType Status { get; set; }

        [BsonElement("FilePath")]
        public string FilePath { get; set; }
    }
}