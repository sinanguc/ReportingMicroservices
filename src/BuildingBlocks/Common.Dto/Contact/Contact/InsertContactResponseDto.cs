using Common.Dto.Contact.Enums;
using System;
using System.Collections.Generic;

namespace Common.Dto.Contact
{
    public class InsertContactResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public List<ContactInfoDto.WithId> PersonContactInfo { get; set; }
    }
}
