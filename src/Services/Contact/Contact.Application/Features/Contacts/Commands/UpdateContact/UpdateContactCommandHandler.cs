using AutoMapper;
using Common.Dto.Contact;
using Contact.Application.Contracts.Persistence;
using Contact.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, UpdateContactResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public UpdateContactCommandHandler(IMapper mapper, IContactRepository contactRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        public async Task<UpdateContactResponseDto> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var personEntity = _mapper.Map<Person>(request.UpdateContactRequestDto);
            await _contactRepository.UpdateAsync(personEntity, cancellationToken);
            return _mapper.Map<UpdateContactResponseDto>(personEntity);
        }
    }
}
