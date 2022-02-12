using Common.Helpers.Pagination;
using Report.API.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Report.API.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<PagedResult<Entities.Report>> GetReportsAsync(CancellationToken cancellicationToken);
        Task<Entities.Report> GenerateReportAsync(Entities.Report report, CancellationToken cancellicationToken);

        Task<Entities.Report> GetReportByIdAsync(string id, CancellationToken cancellicationToken);

        Task<ReportTypePrm> GetServiceByIdAsync(Enums.EnumReportType reportTypeId, CancellationToken cancellicationToken);

        Task<IEnumerable<ReportTypePrm>> GetServicesAsync(CancellationToken cancellicationToken);

        Task<bool> UpdateAsync(Entities.Report report, CancellationToken cancellicationToken);
    }
}
