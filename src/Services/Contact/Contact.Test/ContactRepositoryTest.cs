using AutoMapper;
using Common.Dto.Contact;
using Contact.Application.Contracts.Persistence;
using Contact.Domain.Entities;
using System;
using System.Threading;
using Xunit;
using System.Linq;
using static Contact.Test.TestDataHelper;
using Common.Helpers.Pagination;
using Contact.Application.Features.Contacts.Queries.GetContactsList;
using Contact.Application.Features.Contacts.Queries.GetContactReportByLocation;
using Common.Dto.Contact.Filters;
using System.Collections.Generic;

namespace Contact.Test
{
    public class ContactRepositoryTest
    {
        private IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactRepositoryTest()
        {
            _mapper = ConfigHelper.GetAutoMapperConfig();
            _contactRepository = ConfigHelper.GetContactRepository();
        }

        #region Query Tests
        [Fact]
        public async void GetContacts_Should_ReturnPagedResultContactVm()
        {
            //Arrange
            this.InsertContact_InsertContactRequestDto_ReturnInsertContactResponseDto(TestDataHelper.GetInsertContactRequestDto());

            //Act
            var result = await _contactRepository.GetContactsAsync(new ContactFilter(), CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<PagedResult<ContactVm>>(result);
            Assert.True(result.Results.Count > 0);
        }

        [Fact]
        public async void GetContactReportByLocationFilter_Should_ReturnPagedResultContactReportByLocationVm()
        {
            //Arrange
            this.InsertContact_InsertContactRequestDto_ReturnInsertContactResponseDto(TestDataHelper.GetInsertContactRequestDto());

            //Act
            var result = await _contactRepository.GetContactReportByLocationFilter(new ContactReportByLocationFilter(), CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<PagedResult<ContactReportByLocationVm>>(result);
            Assert.True(result.Results.Count > 0);
        }

        [Fact]
        public async void GetContactReportByLocation_Should_ReturnPagedResultContactReportByLocationVm()
        {
            //Arrange
            this.InsertContact_InsertContactRequestDto_ReturnInsertContactResponseDto(TestDataHelper.GetInsertContactRequestDto());

            //Act
            var result = await _contactRepository.GetContactReportByLocation(String.Empty, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<ContactReportByLocationVm>>(result);
            Assert.True(result.Count > 0);
        }
        #endregion

        #region Command Tests
        [Theory]
        [ClassData(typeof(InsertContactClassData))]
        public async void InsertContact_InsertContactRequestDto_ReturnInsertContactResponseDto(InsertContactRequestDto request)
        {
            //Arrange
            var personEntity = _mapper.Map<Person>(request);

            //Act
            var newPerson = await _contactRepository.InsertAsync(personEntity, CancellationToken.None);
            var result = _mapper.Map<InsertContactResponseDto>(newPerson);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<InsertContactResponseDto>(result);
            Assert.Equal(request.Name, result.Name);
            Assert.True(Guid.TryParse(result.Id.ToString(), out var guidId));
        }

        [Theory]
        [ClassData(typeof(UpdateContactClassData))]
        public async void UpdateContact_UpdateContactRequestDto_ReturnUpdateContactResponseDto(UpdateContactRequestDto request)
        {
            //Arrange
            this.InsertContact_InsertContactRequestDto_ReturnInsertContactResponseDto(TestDataHelper.GetInsertContactRequestDto());
            var personList = await _contactRepository.GetContactsAsync(new Common.Dto.Contact.Filters.ContactFilter(), CancellationToken.None);
            request.Id = personList.Results[0].Id;
            request.PersonContactInfo[0].PersonId = personList.Results[0].PersonContactInfo.First().PersonId;
            request.PersonContactInfo[0].Id = personList.Results[0].PersonContactInfo.First().Id;

            _contactRepository.DetachAll(); // InMemoryDb Dispose problem solved

            //Act
            var personEntity = _mapper.Map<Person>(request);
            var oldPerson = await _contactRepository.UpdateAsync(personEntity, CancellationToken.None);
            var result = _mapper.Map<UpdateContactResponseDto>(oldPerson);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UpdateContactResponseDto>(result);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.PersonContactInfo.First().InfoDetail, result.PersonContactInfo.First().InfoDetail);
        }


        [Fact]
        public async void DeleteContact_Should_ReturnTrue()
        {
            //Arrange
            this.InsertContact_InsertContactRequestDto_ReturnInsertContactResponseDto(TestDataHelper.GetInsertContactRequestDto());
            var personEntity = await _contactRepository.FirstOrDefaultAsync(cancellationToken: CancellationToken.None);

            //Act
            var result = await _contactRepository.DeleteAsync(personEntity, CancellationToken.None);

            //Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
        }
        #endregion
    }
}
