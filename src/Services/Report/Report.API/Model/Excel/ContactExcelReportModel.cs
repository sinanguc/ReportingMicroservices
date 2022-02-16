using System.ComponentModel;

namespace Report.API.Model.Excel
{
    public class ContactExcelReportModel
    {
        [DisplayName("Location Name")]
        public string LocationName { get; set; }

        [DisplayName("Person Count In Location")]
        public long PersonCountInLocation { get; set; }

        [DisplayName("Phone Number Count In Location")]
        public long PhoneNumberCountInLocation { get; set; }
    }
}
