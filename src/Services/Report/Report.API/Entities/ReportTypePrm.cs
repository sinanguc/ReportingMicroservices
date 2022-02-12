using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Report.API.Enums;

namespace Report.API.Entities
{
    public partial class ReportTypePrm
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ReportId")]
        public Enums.EnumReportType ReportId { get; set; }

        [BsonElement("ServiceId")]
        public EnumServiceType ServiceId { get; set; }

        [BsonElement("ReportName")]
        public string ReportName { get; set; }

        [BsonElement("DestinationSavePath")]
        public string DestinationSavePath { get; set; }
    }
}
