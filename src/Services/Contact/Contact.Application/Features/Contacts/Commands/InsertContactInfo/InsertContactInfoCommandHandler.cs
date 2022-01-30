using AutoMapper;
using Common.Dto.Contact;
using Contact.Application.Contracts.Persistence;
using Contact.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.Contacts.Commands.InsertContactInfo
{
    public class InsertContactInfoCommandHandler : IRequestHandler<InsertContactInfoCommand, InsertContactInfoResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IContactInfoRepository _contactInfoRepository;

        public InsertContactInfoCommandHandler(IMapper mapper, IContactInfoRepository contactInfoRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contactInfoRepository = contactInfoRepository ?? throw new ArgumentNullException(nameof(contactInfoRepository));
        }

        public async Task<InsertContactInfoResponseDto> Handle(InsertContactInfoCommand request, CancellationToken cancellationToken)
        {
            var PersonContactInfoEntity = _mapper.Map<PersonContactInfo>(request.InsertContactInfoRequestDto);
            var contactInfoList = await _contactInfoRepository.InsertAsync(PersonContactInfoEntity);
            return _mapper.Map<InsertContactInfoResponseDto>(contactInfoList);
        }
    }
}
