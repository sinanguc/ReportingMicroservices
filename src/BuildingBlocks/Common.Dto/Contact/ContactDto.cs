using System;
using System.Collections.Generic;

namespace Common.Dto.Contact
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public List<ContactDetailDto.WithId> PersonContactInfo { get; set; }
    }
}
