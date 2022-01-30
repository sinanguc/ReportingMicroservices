using MediatR;
using System;

namespace Contact.Application.Features.Contacts.Commands.DeleteContactInfo
{
    public class DeleteContactInfoCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public DeleteContactInfoCommand(Guid? id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}
