using AutoMapper;
using Common.Dto.Contact;
using Common.Helpers.Pagination;
using Contact.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.Contacts.Queries.GetContactsList
{
    public class GetContactsListQueryHandler : IRequestHandler<GetContactsListQuery, PagedResult<ContactDto>>
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public GetContactsListQueryHandler(IMapper mapper, IContactRepository contactRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        public async Task<PagedResult<ContactDto>> Handle(GetContactsListQuery request, CancellationToken cancellationToken)
        {
            var contactList = await _contactRepository.GetContactsAsync(request.Filter);
            return _mapper.Map<PagedResult<ContactDto>>(contactList);
        }
    }
}
