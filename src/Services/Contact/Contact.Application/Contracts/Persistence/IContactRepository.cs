using Common.Dto.Shared;
using Common.Helpers.Pagination;
using Contact.Application.Features.Contacts.Queries.GetContactsList;
using Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.Application.Contracts.Persistence
{
    public interface IContactRepository : IAsyncRepository<Person>
    {
        Task<IEnumerable<Person>> GetPersonAsync();

        Task<IEnumerable<Person>> GetPersonByPersonIdAsync(Guid personId);

        Task<PagedResult<ContactVm>> GetContactsAsync(GenericFilter filter);
    }
}
