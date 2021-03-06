using Common.Dto.Contact.Filters;
using Common.Dto.Shared;
using Common.Helpers.Pagination;
using Contact.Application.Features.Contacts.Queries.GetContactReportByLocation;
using Contact.Application.Features.Contacts.Queries.GetContactsList;
using Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Contracts.Persistence
{
    public interface IContactRepository : IAsyncRepository<Person>
    {

        Task<PagedResult<ContactVm>> GetContactsAsync(ContactFilter filter, CancellationToken cancellationToken);

        Task<PagedResult<ContactReportByLocationVm>> GetContactReportByLocationFilter(ContactReportByLocationFilter filter, CancellationToken cancellationToken);

        Task<List<ContactReportByLocationVm>> GetContactReportByLocation(string locationName, CancellationToken cancellationToken);
    }
}
