namespace Contact.Application.Features.Contacts.Queries.GetContactReportByLocation
{
    public class ContactReportByLocationVm
    {
        public string LocationName { get; set; }
        public long PersonCountInLocation { get; set; }
        public long PhoneNumberCountInLocation { get; set; }
    }
}
