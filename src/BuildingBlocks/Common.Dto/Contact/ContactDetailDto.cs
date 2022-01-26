using Common.Dto.Contact.Enums;
using System;

namespace Common.Dto.Contact
{
    public class ContactDetailDto
    {
        public EnumContactInfoTypeDto InfoType { get; set; }
        public string InfoDetail { get; set; }

        public class WithId : ContactDetailDto
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
        }
    }
}
