using AutoMapper;
using Common.Dto.Contact;
using Common.Helpers.Pagination;
using Contact.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Features.Contacts.Queries.GetContactReportByLocation
{
    public class GetContactReportByLocationQueryHandler : IRequestHandler<GetContactReportByLocationQuery, PagedResult<ContactReportByLocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public GetContactReportByLocationQueryHandler(IMapper mapper, IContactRepository contactRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        public async Task<PagedResult<ContactReportByLocationDto>> Handle(GetContactReportByLocationQuery request, CancellationToken cancellationToken)
        {
            var contactReport = await _contactRepository.GetContactReportByLocation(request.Filter.LocationName, cancellationToken);
            return _mapper.Map<PagedResult<ContactReportByLocationDto>>(contactReport);
        }
    }


    public class GetContactReportByLocationForExportFileQueryHandler : IRequestHandler<GetContactReportByLocationForExportFileQuery, List<ContactReportByLocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public GetContactReportByLocationForExportFileQueryHandler(IMapper mapper, IContactRepository contactRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        public async Task<List<ContactReportByLocationDto>> Handle(GetContactReportByLocationForExportFileQuery request, CancellationToken cancellationToken)
        {
            var contactReport = await _contactRepository.GetContactReportByLocation(request.LocationName, cancellationToken);
            return _mapper.Map<List<ContactReportByLocationDto>>(contactReport);
        }
    }
}
