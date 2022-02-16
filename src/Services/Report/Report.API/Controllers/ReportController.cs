using AutoMapper;
using Common.Dto.Report.Report;
using Common.Dto.Shared;
using Common.Helpers.Pagination;
using Common.Messages;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Report.API.Configuration;
using Report.API.GrpcServices;
using Report.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Report.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IReportRepository _reportRepository;

        public ReportController(ILogger<ReportController> logger, IMapper mapper, IPublishEndpoint publishEndpoint, IReportRepository reportRepository)
        {
            _logger = logger;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
        }

        [HttpGet("GetReports")]
        public async Task<GenericResult> GetReports([FromQuery] ReportFilter filter, CancellationToken cancellicationToken)
        {
            var reports = await _reportRepository.GetReportsAsync(filter: filter, cancellicationToken: cancellicationToken);

            reports.Results.ToList().ForEach(d => d.FilePath = $"{ReportAppConfiguration.GetFileServerUrl()}/{d.FilePath}");

            _logger.LogInformation("GetReports Listed");
            GenericResult result = new();
            result.Data = _mapper.Map<PagedResult<ReportDto>>(reports);
            result.Message = GenericMessages.Successfully_Listed;
            return result;
        }

        [HttpPost("CreateReport")]
        public async Task<GenericResult> CreateReport([FromBody] InsertReportRequestDto insertReportRequestDto, CancellationToken cancellicationToken)
        {
            var reportEntity = _mapper.Map<Entities.Report>(insertReportRequestDto);
            reportEntity = await _reportRepository.InsertReportAsync(report: reportEntity, cancellicationToken: cancellicationToken);

            _logger.LogInformation("Report Created");

            var eventMessage = _mapper.Map<ReportBackgroundServiceEvent>(reportEntity);
            await _publishEndpoint.Publish<ReportBackgroundServiceEvent>(eventMessage);

            _logger.LogInformation("Report Published to Queue");

            GenericResult result = new();
            result.Data = _mapper.Map<InsertReportResponseDto>(reportEntity);
            result.Message = GenericMessages.Successfully_Registered;
            return result;
        }

    }
}
