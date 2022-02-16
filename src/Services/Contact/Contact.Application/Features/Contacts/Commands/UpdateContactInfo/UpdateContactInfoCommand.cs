using Common.Dto.Contact;
using MediatR;
using System;

namespace Contact.Application.Features.Contacts.Commands.UpdateContactInfo
{
    public class UpdateContactInfoCommand : IRequest<UpdateContactInfoResponseDto>
    {
        public UpdateContactInfoRequestDto UpdateContactInfoRequestDto { get; set; }
        public UpdateContactInfoCommand(UpdateContactInfoRequestDto updateContactInfoRequestDto)
        {
            UpdateContactInfoRequestDto = updateContactInfoRequestDto ?? throw new ArgumentNullException(nameof(updateContactInfoRequestDto));
        }
    }
}
