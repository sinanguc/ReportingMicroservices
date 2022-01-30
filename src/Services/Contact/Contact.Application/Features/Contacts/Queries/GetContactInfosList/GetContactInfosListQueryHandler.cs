using AutoMapper;
using Common.Dto.Contact;
using Common.Helpers.Pagination;
using Contact.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.Contacts.Queries.GetContactInfosList
{
    public class GetContactInfosListQueryHandler : IRequestHandler<GetContactInfosListQuery, PagedResult<ContactInfoDto>>
    {
        private readonly IMapper _mapper;
        private readonly IContactInfoRepository _contactInfoRepository;

        public GetContactInfosListQueryHandler(IMapper mapper, IContactInfoRepository contactInfoRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contactInfoRepository = contactInfoRepository ?? throw new ArgumentNullException(nameof(contactInfoRepository));
        }

        public async Task<PagedResult<ContactInfoDto>> Handle(GetContactInfosListQuery request, CancellationToken cancellationToken)
        {
            var contactInfoList = await _contactInfoRepository.GetContactInfosAsync(request.Filter);
            return _mapper.Map<PagedResult<ContactInfoDto>>(contactInfoList);
        }
    }
}
