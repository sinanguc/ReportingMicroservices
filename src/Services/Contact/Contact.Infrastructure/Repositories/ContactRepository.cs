using Common.Dto.Shared;
using Common.Helpers.Pagination;
using Contact.Application.Contracts.Persistence;
using Contact.Application.Features.Contacts.Queries.GetContactsList;
using Contact.Domain.Entities;
using Contact.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Repositories
{
    public class ContactRepository : BaseRepository<Person>, IContactRepository
    {
        public ContactRepository(ContactContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<Person>> GetPersonAsync()
        {
            var contactList = await _dbContext.Persons.ToListAsync();
            return contactList;
        }

        public Task<Person> GetPersonByPersonIdAsync(Guid personId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<ContactVm>> GetContactsAsync(GenericFilter filter)
        {
            var contactList = (from person in _dbContext.Persons
                               join contact in _dbContext.PersonContactInfos on person.Id equals contact.PersonId into personContact
                               from contact in personContact.DefaultIfEmpty()
                               select new ContactVm
                               {
                                   Id = person.Id,
                                   Name = person.Name,
                                   Surname = person.Surname,
                                   Company = person.Company,
                                   PersonContactInfo = person.PersonContactInfo.Select(d =>
                                   new ContactDetailVm()
                                   {
                                       Id = d.Id,
                                       InfoDetail = d.InfoDetail,
                                       InfoType = d.InfoType,
                                       PersonId = person.Id
                                   }),
                               });

            return await PaginationHelper.GetPagedAsync<ContactVm>(query: contactList, page: filter.CurrentPage, pageSize: filter.PageSize);
        }

    }
}
