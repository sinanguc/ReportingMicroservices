using Common.Dto.Report.Report;
using Common.Helpers.Pagination;
using MongoDB.Driver;
using Report.API.Data.Interfaces;
using Report.API.Entities;
using Report.API.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Report.API.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly IReportContext _context;

        public ReportRepository(IReportContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Entities.Report>> GetReportsAsync(ReportFilter filter, CancellationToken cancellicationToken)
        {
            var result = await _context.Reports.AsQueryable().GetPagedAsync(page: filter.CurrentPage, pageSize: filter.PageSize);
            return result;
        }

        public async Task<Entities.Report> GetReportByIdAsync(string id, CancellationToken cancellicationToken)
        {
            return await _context.Reports.Find(d => d.Id == id).FirstOrDefaultAsync(cancellationToken: cancellicationToken);
        }

        public async Task<ReportTypePrm> GetReportTypeByIdAsync(Enums.EnumReportType reportTypeId, CancellationToken cancellicationToken)
        {
            return await _context.ReportTypePrms.Find(d => d.ReportId == reportTypeId).FirstOrDefaultAsync(cancellationToken: cancellicationToken);
        }

        public async Task<Entities.Report> InsertReportAsync(Entities.Report report, CancellationToken cancellicationToken)
        {
            report.Status = Enums.EnumReportStatusType.Preparing;
            report.RequestDate = DateTime.Now;
            await _context.Reports.InsertOneAsync(document: report, cancellationToken: cancellicationToken);
            return report;
        }

        public async Task<bool> UpdateReportAsync(Entities.Report report, CancellationToken cancellicationToken)
        {
            var updateResult = await _context.Reports.ReplaceOneAsync(
                filter: d => d.Id == report.Id, 
                replacement: report, 
                cancellationToken: cancellicationToken);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
