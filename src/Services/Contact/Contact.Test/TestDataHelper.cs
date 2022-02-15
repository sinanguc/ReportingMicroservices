using Common.Dto.Contact;
using Common.Dto.Contact.Enums;
using System;
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
                    GetInsertContactRequestDto()
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class UpdateContactClassData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    GetUpdateContactRequestDto()
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


        public static InsertContactRequestDto GetInsertContactRequestDto()
        {
            return new InsertContactRequestDto()
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
            };
        }

        public static UpdateContactRequestDto GetUpdateContactRequestDto()
        {
            return new UpdateContactRequestDto()
            {
                Name = "Sinan",
                Surname = "Güç",
                Company = "Netcad",
                PersonContactInfo = new List<ContactInfoDto.WithId>()
                        {
                            new ContactInfoDto.WithId()
                            {
                                InfoType = EnumContactInfoTypeDto.Location,
                                InfoDetail = "İstanbul"
                            }
                        }
            };
        }
    }
}
