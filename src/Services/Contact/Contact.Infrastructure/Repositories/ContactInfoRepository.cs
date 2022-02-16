using Common.Dto.Contact.Filters;
using Common.Helpers.Pagination;
using Contact.Application.Contracts.Persistence;
using Contact.Domain.Entities;
using Contact.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Repositories
{
    public class ContactInfoRepository : BaseRepository<PersonContactInfo>, IContactInfoRepository
    {
        public ContactInfoRepository(ContactContext dbContext)
            : base(dbContext)
        { }

        public async Task<PagedResult<PersonContactInfo>> GetContactInfosAsync(ContactInfoFilter filter, CancellationToken cancellationToken)
        {
            return await _dbContext.PersonContactInfos.GetPagedAsync(page: filter.CurrentPage, pageSize: filter.PageSize);
        }
    }
}
