using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contact.Application.Contracts.Persistence;
using MediatR;

namespace Contact.Application.Features.Contacts.Commands.DeleteContact
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public DeleteContactCommandHandler(IMapper mapper, IContactRepository contactRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        public async Task<Guid> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var personEntity = await _contactRepository.FirstOrDefaultAsync(cancellationToken, d => d.Id == request.Id);
            await _contactRepository.DeleteAsync(personEntity, cancellationToken);
            return request.Id;
        }
    }
}
