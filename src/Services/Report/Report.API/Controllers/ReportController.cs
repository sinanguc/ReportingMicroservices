using AutoMapper;
using Common.Dto.Report.Report;
using Common.Dto.Shared;
using Common.Helpers.Pagination;
using Common.Messages;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Report.API.GrpcServices;
using Report.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Report.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IReportRepository _reportRepository;

        public ReportController(IMapper mapper, IPublishEndpoint publishEndpoint, IReportRepository reportRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
        }

        [HttpGet("GetReports")]
        public async Task<GenericResult> GetReports([FromQuery] ReportFilter filter, CancellationToken cancellicationToken)
        {
            var reports = await _reportRepository.GetReportsAsync(filter: filter, cancellicationToken: cancellicationToken);

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

            var eventMessage = _mapper.Map<ReportBackgroundServiceEvent>(reportEntity);
            await _publishEndpoint.Publish<ReportBackgroundServiceEvent>(eventMessage);

            GenericResult result = new();
            result.Data = _mapper.Map<InsertReportResponseDto>(reportEntity);
            result.Message = GenericMessages.Successfully_Registered;
            return result;
        }

    }
}
