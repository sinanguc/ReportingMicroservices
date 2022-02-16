using Common.Dto.Contact;
using MediatR;
using System;

namespace Contact.Application.Features.Contacts.Commands.InsertContactInfo
{
    public class InsertContactInfoCommand : IRequest<InsertContactInfoResponseDto>
    {
        public InsertContactInfoRequestDto InsertContactInfoRequestDto { get; set; }
        public InsertContactInfoCommand(InsertContactInfoRequestDto insertContactInfoRequestDto)
        {
            InsertContactInfoRequestDto = insertContactInfoRequestDto ?? throw new ArgumentNullException(nameof(insertContactInfoRequestDto));
        }
    }
}
