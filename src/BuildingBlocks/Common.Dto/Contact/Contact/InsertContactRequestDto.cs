using System.Collections.Generic;

namespace Common.Dto.Contact
{
    public class InsertContactRequestDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public List<ContactInfoDto> PersonContactInfo { get; set; }
    }
}
