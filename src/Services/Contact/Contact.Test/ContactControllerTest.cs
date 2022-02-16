using AutoMapper;
using Common.Dto.Contact;
using Contact.API.Controllers;
using Contact.Application.Contracts.Persistence;
using Contact.Application.Features.Contacts.Commands.DeleteContact;
using Contact.Application.Features.Contacts.Commands.InsertContact;
using Contact.Application.Features.Contacts.Commands.UpdateContact;
using MediatR;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Contact.Test.TestDataHelper;

namespace Contact.Test
{
    public class ContactControllerTest
    {
        private IMapper _mapper;
        private IContactRepository _contactRepository;

        public ContactControllerTest()
        {
            _mapper = ConfigHelper.GetAutoMapperConfig();
            _contactRepository = ConfigHelper.GetContactRepository();
        }

        [Theory]
        [ClassData(typeof(InsertContactClassData))]
        public async void AddContact_Should_ReturnInsertContactResponseDto(InsertContactRequestDto request)
        {
            //Arrange

            //Act
            var command = new InsertContactCommand(request);
            var handler = new InsertContactCommandHandler(mapper: _mapper, contactRepository: _contactRepository);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<InsertContactResponseDto>(result);
            Assert.Equal(request.Name, result.Name);
            Assert.True(Guid.TryParse(result.Id.ToString(), out var guidId));
        }

        [Theory]
        [ClassData(typeof(UpdateContactClassData))]
        public async void UpdateContact_Should_ReturnUpdateContactResponseDto(UpdateContactRequestDto request)
        {
            //Arrange
            AddContact_Should_ReturnInsertContactResponseDto(TestDataHelper.GetInsertContactRequestDto());
            var person = await _contactRepository.FirstOrDefaultAsync(cancellationToken: CancellationToken.None);
            request.Id = person.Id;
            request.PersonContactInfo.First().PersonId = person.Id;
            request.PersonContactInfo.First().Id = person.PersonContactInfo.First().Id;
            _contactRepository.DetachAll();

            //Act
            var command = new UpdateContactCommand(request);
            var handler = new UpdateContactCommandHandler(mapper: _mapper, contactRepository: _contactRepository);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UpdateContactResponseDto>(result);
            Assert.Equal(request.Name, result.Name);

            Assert.NotNull(result.PersonContactInfo);
            Assert.True(result.PersonContactInfo.Count > 0);
            Assert.Equal(request.PersonContactInfo.First().InfoDetail, result.PersonContactInfo.First().InfoDetail);
        }

        [Fact]
        public async void DeleteContact_Should_ReturnGuid()
        {
            //Arrange
            AddContact_Should_ReturnInsertContactResponseDto(TestDataHelper.GetInsertContactRequestDto());
            var person = await _contactRepository.FirstOrDefaultAsync(cancellationToken: CancellationToken.None);
            _contactRepository.DetachAll();

            //Act
            var command = new DeleteContactCommand(person.Id);
            var handler = new DeleteContactCommandHandler(mapper: _mapper, contactRepository: _contactRepository);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Guid>(result);
            Assert.Equal(person.Id, result);
        }
    }
}
