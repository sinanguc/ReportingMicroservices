using MediatR;
using System;

namespace Contact.Application.Features.Contacts.Commands.DeleteContact
{
    public class DeleteContactCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public DeleteContactCommand(Guid? id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}
