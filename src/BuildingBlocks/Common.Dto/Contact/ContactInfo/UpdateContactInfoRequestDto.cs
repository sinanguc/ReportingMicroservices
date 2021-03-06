using Common.Dto.Contact.Enums;
using System;

namespace Common.Dto.Contact
{
    public class UpdateContactInfoRequestDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public EnumContactInfoTypeDto InfoType { get; set; }
        public string InfoDetail { get; set; }
    }
}
