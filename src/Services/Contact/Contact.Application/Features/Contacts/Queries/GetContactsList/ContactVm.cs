using System;

namespace Contact.Application.Features.Contacts.Queries.GetContactsList
{
    public class ContactVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactDetailVm ContactDetail { get; set; }
    }

    public class ContactDetailVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
