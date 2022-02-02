using AutoMapper;
using Common.Dto.Contact;
using Contact.Application.Contracts.Persistence;
using Contact.Domain.Entities;
using System;
using System.Threading;
using Xunit;
using static Contact.Test.TestDataHelper;

namespace Contact.Test
{
    public class ContactRepositoryTest
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactRepositoryTest()
        {
            _mapper = ConfigHelper.GetAutoMapperConfig();
            _contactRepository = ConfigHelper.GetContactRepository();
        }

        [Theory]
        [ClassData(typeof(InsertContactClassData))]
        public async void InsertContact_InsertContactRequestDto_ReturnInsertContactResponseDto(InsertContactRequestDto request)
        {
            var personEntity = _mapper.Map<Person>(request);
            var newPerson = await _contactRepository.InsertAsync(personEntity, CancellationToken.None);
            var result = _mapper.Map<InsertContactResponseDto>(newPerson);

            Assert.NotNull(result);
            Assert.IsType<InsertContactResponseDto>(result);
            Assert.Equal("Bilgehan Sinan", result.Name);
            Assert.True(Guid.TryParse(result.Id.ToString(), out var guidId));
        }
    }
}
