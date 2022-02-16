using Common.Dto.Contact;
using Common.Dto.Contact.Filters;
using Common.Helpers.Pagination;
using MediatR;
using System;

namespace Contact.Application.Features.Contacts.Queries.GetContactsList
{
    public class GetContactsListQuery : IRequest<PagedResult<ContactDto>>
    {
        public ContactFilter Filter { get; set; }

        public GetContactsListQuery(ContactFilter filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }
    }
}
