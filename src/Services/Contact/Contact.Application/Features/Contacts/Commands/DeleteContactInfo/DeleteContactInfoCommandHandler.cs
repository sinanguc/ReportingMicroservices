using AutoMapper;
using Contact.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.Contacts.Commands.DeleteContactInfo
{
    public class DeleteContactInfoCommandHandler : IRequestHandler<DeleteContactInfoCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IContactInfoRepository _contactInfoRepository;

        public DeleteContactInfoCommandHandler(IMapper mapper, IContactInfoRepository contactInfoRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contactInfoRepository = contactInfoRepository ?? throw new ArgumentNullException(nameof(contactInfoRepository));
        }

        public async Task<Guid> Handle(DeleteContactInfoCommand request, CancellationToken cancellationToken)
        {
            var personEntity = await _contactInfoRepository.GetByIdAsync(request.Id);
            await _contactInfoRepository.DeleteAsync(personEntity);
            return request.Id;
        }
    }
}
