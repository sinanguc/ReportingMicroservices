using Common.Dto.Contact;
using MediatR;
using System;

namespace Contact.Application.Features.Contacts.Commands.InsertContact
{
    public class InsertContactCommand : IRequest<InsertContactDto>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
    }
}
