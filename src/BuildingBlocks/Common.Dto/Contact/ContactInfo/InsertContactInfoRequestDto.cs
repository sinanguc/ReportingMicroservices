using Common.Dto.Contact.Enums;
using System;

namespace Common.Dto.Contact
{
    public class InsertContactInfoRequestDto
    {
        public Guid PersonId { get; set; }
        public EnumContactInfoTypeDto InfoType { get; set; }
        public string InfoDetail { get; set; }
    }
}
