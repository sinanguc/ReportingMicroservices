using Common.Dto.Contact;
using Common.Dto.Contact.Filters;
using Common.Helpers.Pagination;
using MediatR;
using System;
using System.Collections.Generic;

namespace Contact.Application.Features.Contacts.Queries.GetContactReportByLocation
{
    public class GetContactReportByLocationQuery : IRequest<PagedResult<ContactReportByLocationDto>>
    {
        public ContactReportByLocationFilter Filter { get; set; }

        public GetContactReportByLocationQuery(ContactReportByLocationFilter filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }
    }

    public class GetContactReportByLocationForExportFileQuery : IRequest<List<ContactReportByLocationDto>>
    {
        public string LocationName { get; set; }

        public GetContactReportByLocationForExportFileQuery(string locationName)
        {
            LocationName = locationName;
        }
    }
}
