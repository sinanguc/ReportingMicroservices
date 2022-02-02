using Common.Dto.Contact.Filters;
using Common.Helpers.Pagination;
using Contact.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Contact.Application.Contracts.Persistence
{
    public interface IContactInfoRepository : IAsyncRepository<PersonContactInfo>
    {
        Task<PagedResult<PersonContactInfo>> GetContactInfosAsync(ContactInfoFilter filter, CancellationToken cancellationToken);
    }
}
