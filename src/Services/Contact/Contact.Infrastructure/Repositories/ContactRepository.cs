using Common.Dto.Contact.Filters;
using Common.Dto.Shared;
using Common.Helpers.Pagination;
using Contact.Application.Contracts.Persistence;
using Contact.Application.Features.Contacts.Queries.GetContactReportByLocation;
using Contact.Application.Features.Contacts.Queries.GetContactsList;
using Contact.Domain.Entities;
using Contact.Domain.Enums;
using Contact.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Repositories
{
    public class ContactRepository : BaseRepository<Person>, IContactRepository
    {
        public ContactRepository(ContactContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<PagedResult<ContactVm>> GetContactsAsync(ContactFilter filter, CancellationToken cancellationToken)
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

        public async Task<PagedResult<ContactReportByLocationVm>> GetContactReportByLocationFilter(ContactReportByLocationFilter filter, CancellationToken cancellationToken)
        {
            var report = (from contact in _dbContext.PersonContactInfos
                          where contact.InfoType == ContactInfoType.Location
                          group contact by new
                          {
                              contact.InfoDetail
                          } into contactGroup
                          select new ContactReportByLocationVm
                          {
                              LocationName = contactGroup.Key.InfoDetail,
                              PersonCountInLocation = (from c in _dbContext.PersonContactInfos
                                                       where c.InfoDetail == contactGroup.Key.InfoDetail
                                                       select c).Count(),
                              PhoneNumberCountInLocation = (from c in (from c in _dbContext.PersonContactInfos
                                                                       where c.InfoType == ContactInfoType.Location
                                                                       select c)
                                                            join d in (from d in _dbContext.PersonContactInfos
                                                                       where d.InfoType == ContactInfoType.PhoneNumber
                                                                       select d) on c.PersonId equals d.PersonId
                                                            where c.InfoDetail == contactGroup.Key.InfoDetail
                                                            select d).Count()
                          }).Distinct();

            if (!string.IsNullOrWhiteSpace(filter.LocationName))
                report = report.Where(d => d.LocationName == filter.LocationName);

            return await PaginationHelper.GetPagedAsync<ContactReportByLocationVm>(query: report, page: filter.CurrentPage, pageSize: filter.PageSize);

        }

        public async Task<List<ContactReportByLocationVm>> GetContactReportByLocation(string locationName, CancellationToken cancellationToken)
        {
            var report = (from contact in _dbContext.PersonContactInfos
                          where contact.InfoType == ContactInfoType.Location
                          group contact by new
                          {
                              contact.InfoDetail
                          } into contactGroup
                          select new ContactReportByLocationVm
                          {
                              LocationName = contactGroup.Key.InfoDetail,
                              PersonCountInLocation = (from c in _dbContext.PersonContactInfos 
                                                       where c.InfoDetail == contactGroup.Key.InfoDetail 
                                                       select c).Count(),
                              PhoneNumberCountInLocation = (from c in (from c in _dbContext.PersonContactInfos 
                                                                       where c.InfoType == ContactInfoType.Location 
                                                                       select c)
                                                            join d in (from d in _dbContext.PersonContactInfos 
                                                                       where d.InfoType == ContactInfoType.PhoneNumber 
                                                                       select d) on c.PersonId equals d.PersonId
                                                            where c.InfoDetail == contactGroup.Key.InfoDetail
                                                            select d).Count()
                          }).Distinct();

            if (!string.IsNullOrWhiteSpace(locationName))
                report = report.Where(d => d.LocationName == locationName);

            return await Task.FromResult(report.ToList());

        }
    }
}
