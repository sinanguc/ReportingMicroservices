using Common.Dto.Contact;
using Common.Dto.Contact.Enums;
using System.Collections;
using System.Collections.Generic;

namespace Contact.Test
{
    public class TestDataHelper
    {
        public class InsertContactClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new InsertContactRequestDto()
                    {
                        Name = "Bilgehan Sinan",
                        Surname = "Güç",
                        Company = "Netcad",
                        PersonContactInfo = new List<ContactInfoDto>()
                        {
                            new ContactInfoDto()
                            {
                                InfoType = EnumContactInfoTypeDto.Location,
                                InfoDetail = "Ankara"
                            }
                        }
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
