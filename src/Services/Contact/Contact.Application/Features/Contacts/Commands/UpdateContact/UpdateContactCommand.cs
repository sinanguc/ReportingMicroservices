using Common.Dto.Contact;
using MediatR;
using System;

namespace Contact.Application.Features.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommand : IRequest<UpdateContactResponseDto>
    {
        public UpdateContactRequestDto UpdateContactRequestDto { get; set; }

        public UpdateContactCommand(UpdateContactRequestDto updateContactRequestDto)
        {
            UpdateContactRequestDto = updateContactRequestDto ?? throw new ArgumentNullException(nameof(updateContactRequestDto));
        }
    }
}
