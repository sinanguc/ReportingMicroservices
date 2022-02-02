using AutoMapper;
using Common.Dto.Contact;
using Contact.API.Controllers;
using Contact.Application.Contracts.Persistence;
using Contact.Application.Features.Contacts.Commands.InsertContact;
using MediatR;
using Moq;
using System;
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
        public async void AddContact_InsertContactRequestDto_ReturnInsertContactResponseDto(InsertContactRequestDto request)
        {
            var command = new InsertContactCommand(request);
            var handler = new InsertContactCommandHandler(mapper: _mapper, contactRepository: _contactRepository);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<InsertContactResponseDto>(result);
            Assert.Equal("Bilgehan Sinan", result.Name);
            Assert.True(Guid.TryParse(result.Id.ToString(), out var guidId));
        }
    }
}
