using Common.Dto.Report.Report;
using Common.Helpers.Pagination;
using Report.API.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Report.API.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<PagedResult<Entities.Report>> GetReportsAsync(ReportFilter filter, CancellationToken cancellicationToken);

        Task<Entities.Report> InsertReportAsync(Entities.Report report, CancellationToken cancellicationToken);

        Task<Entities.Report> GetReportByIdAsync(string id, CancellationToken cancellicationToken);

        Task<ReportTypePrm> GetReportTypeByIdAsync(Enums.EnumReportType reportTypeId, CancellationToken cancellicationToken);

        Task<bool> UpdateReportAsync(Entities.Report report, CancellationToken cancellicationToken);
    }
}
