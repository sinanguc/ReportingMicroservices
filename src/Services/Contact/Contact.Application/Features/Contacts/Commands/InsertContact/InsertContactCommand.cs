using Common.Dto.Contact;
using MediatR;
using System;

namespace Contact.Application.Features.Contacts.Commands.InsertContact
{
    public class InsertContactCommand : IRequest<InsertContactResponseDto>
    {
        public InsertContactRequestDto InsertContactRequestDto { get; set; }

        public InsertContactCommand(InsertContactRequestDto insertContactRequestDto)
        {
            InsertContactRequestDto = insertContactRequestDto ?? throw new ArgumentNullException(nameof(insertContactRequestDto));
        }
    }
}
