using Common.Dto.Contact.Enums;
using System;

namespace Common.Dto.Contact
{
    public class ContactInfoDto
    {
        public EnumContactInfoTypeDto InfoType { get; set; }
        public string InfoDetail { get; set; }

        public class WithId : ContactInfoDto
        {
            public Guid Id { get; set; }
            public Guid PersonId { get; set; }
        }
    }
}
