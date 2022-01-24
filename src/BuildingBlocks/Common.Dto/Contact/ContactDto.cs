using Common.Dto.Contact.Enums;
using System;

namespace Common.Dto.Contact
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public ContactDetailDto ContactDetail { get; set; }
    }

    public class ContactDetailDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public EnumContactInfoTypeDto InfoType { get; set; }
        public string InfoDetail { get; set; }
    }
}
