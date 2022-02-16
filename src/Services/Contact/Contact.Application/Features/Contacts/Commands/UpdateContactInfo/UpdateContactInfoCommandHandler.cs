using AutoMapper;
using Common.Dto.Contact;
using Contact.Application.Contracts.Persistence;
using Contact.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.Contacts.Commands.UpdateContactInfo
{
    public class UpdateContactInfoCommandHandler : IRequestHandler<UpdateContactInfoCommand, UpdateContactInfoResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IContactInfoRepository _contactInfoRepository;

        public UpdateContactInfoCommandHandler(IMapper mapper, IContactInfoRepository contactInfoRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contactInfoRepository = contactInfoRepository ?? throw new ArgumentNullException(nameof(contactInfoRepository));
        }

        public async Task<UpdateContactInfoResponseDto> Handle(UpdateContactInfoCommand request, CancellationToken cancellationToken)
        {
            var personContactInfoEntity = _mapper.Map<PersonContactInfo>(request.UpdateContactInfoRequestDto);
            await _contactInfoRepository.UpdateAsync(personContactInfoEntity, cancellationToken);
            return _mapper.Map<UpdateContactInfoResponseDto>(personContactInfoEntity);
        }
    }
}
