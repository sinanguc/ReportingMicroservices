using Contact.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Contact.Application.Features.Contacts.Queries.GetContactsList
{
    public class ContactVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public IEnumerable<ContactDetailVm> PersonContactInfo { get; set; }
    }

    public class ContactDetailVm
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public ContactInfoType InfoType { get; set; }
        public string InfoDetail { get; set; }
    }
}
