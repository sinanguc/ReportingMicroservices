using AutoMapper;
using Common.Dto.Contact;
using Contact.Application.Contracts.Persistence;
using Contact.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.Contacts.Commands.InsertContact
{
    public class InsertContactCommandHandler : IRequestHandler<InsertContactCommand, InsertContactResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public InsertContactCommandHandler(IMapper mapper, IContactRepository contactRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        public async Task<InsertContactResponseDto> Handle(InsertContactCommand request, CancellationToken cancellationToken)
        {
            var personEntity = _mapper.Map<Person>(request.InsertContactRequestDto);
            var newPerson = await _contactRepository.InsertAsync(personEntity);
            return _mapper.Map<InsertContactResponseDto>(newPerson);
        }
    }
}
