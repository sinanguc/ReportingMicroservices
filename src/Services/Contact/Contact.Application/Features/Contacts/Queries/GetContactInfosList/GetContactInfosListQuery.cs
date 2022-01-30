using Common.Dto.Contact;
using Common.Dto.Contact.Filters;
using Common.Helpers.Pagination;
using MediatR;
using System;

namespace Contact.Application.Features.Contacts.Queries.GetContactInfosList
{
    public class GetContactInfosListQuery : IRequest<PagedResult<ContactInfoDto>>
    {
        public ContactInfoFilter Filter { get; set; }

        public GetContactInfosListQuery(ContactInfoFilter filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }
    }
}
