namespace Common.Dto.Contact
{
    public class ContactReportByLocationDto
    {
        public string LocationName { get; set; }
        public long PersonCountInLocation { get; set; }
        public long PhoneNumberCountInLocation { get; set; }
    }
}
