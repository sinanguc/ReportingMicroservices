using AutoMapper;
using Common.Dto.Contact;
using Contact.API.Controllers;
using Contact.Application.Contracts.Persistence;
using Contact.Application.Features.Contacts.Commands.InsertContact;
using MediatR;
using Moq;
using System;
using System.Threading;
using Xunit;
using static Contact.Test.TestDataHelper;

namespace Contact.Test
{
    public class ContactControllerTest
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;
        private readonly Mock<IMediator> _mediator;
        private readonly ContactController _controller;

        public ContactControllerTest()
        {
            _mapper = ConfigHelper.GetAutoMapperConfig();
            _contactRepository = ConfigHelper.GetContactRepository();
            _mediator = new Mock<IMediator>();
            _controller = new ContactController(_mediator.Object);
        }

        [Theory]
        [ClassData(typeof(InsertContactClassData))]
        public async void AddContact_InsertContactRequestDto_ReturnInsertContactResponseDto(InsertContactRequestDto request)
        {
            var command = new InsertContactCommand(request);
            var handler = new InsertContactCommandHandler(mapper: _mapper, contactRepository: _contactRepository);
            var result = await handler.Handle(command, CancellationToken.None);

            //var test = await _controller.AddContact(request);

            //_mediator.Verify(x => x.Send(It.Is<InsertContactCommand>(y => y.InsertContactRequestDto.Name == "Bilgehan Sinan"),
            //   It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<InsertContactResponseDto>(result);
            Assert.Equal("Bilgehan Sinan", result.Name);
            Assert.True(Guid.TryParse(result.Id.ToString(), out var guidId));
        }
    }
}
