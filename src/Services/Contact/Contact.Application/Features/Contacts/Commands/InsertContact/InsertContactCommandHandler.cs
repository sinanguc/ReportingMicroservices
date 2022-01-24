using AutoMapper;
using Common.Dto.Contact;
using Contact.Application.Contracts.Persistence;
using Contact.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.Contacts.Commands.InsertContact
{
    public class InsertContactCommandHandler : IRequestHandler<InsertContactCommand, InsertContactDto>
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public InsertContactCommandHandler(IMapper mapper, IContactRepository contactRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        public async Task<InsertContactDto> Handle(InsertContactCommand request, CancellationToken cancellationToken)
        {
            var personEntity = _mapper.Map<Person>(request);
            var newPerson = await _contactRepository.AddAsync(personEntity);
            return _mapper.Map<InsertContactDto>(newPerson);
        }
    }
}
