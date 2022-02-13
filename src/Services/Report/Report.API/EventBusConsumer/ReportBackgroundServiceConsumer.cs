using AutoMapper;
using Common.Helpers.Excel;
using Common.Infrastructure.BackgroundService.Interfaces;
using EventBus.Messages.Events;
using MassTransit;
using Report.API.GrpcServices;
using Report.API.Model.Excel;
using Report.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Report.API.EventBusConsumer
{
    public class ReportBackgroundServiceConsumer : IConsumer<ReportBackgroundServiceEvent>
    {
        private readonly IMapper _mapper;
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ContactGrpcService _contactGrpcService;
        private readonly IReportRepository _reportRepository;

        public ReportBackgroundServiceConsumer(
            IMapper mapper,
            IBackgroundTaskQueue taskQueue,
            ContactGrpcService contactGrpcService, 
            IReportRepository reportRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _taskQueue = taskQueue ?? throw new ArgumentNullException(nameof(taskQueue));
            _contactGrpcService = contactGrpcService ?? throw new ArgumentNullException(nameof(contactGrpcService));
            _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
        }

        public async Task Consume(ConsumeContext<ReportBackgroundServiceEvent> context)
        {
            await _taskQueue.QueueBackgroundWorkItemAsync(async token =>
            {
                await GenerateReport(context, token);
            });
        }

        private async Task GenerateReport(ConsumeContext<ReportBackgroundServiceEvent> context, CancellationToken cancellationToken)
        {
            var reportEntity = _mapper.Map<Entities.Report>(context.Message);

            var report = await _reportRepository.GetReportByIdAsync(reportEntity.Id, cancellationToken);

            var reportTypePrm = await _reportRepository.GetReportTypeByIdAsync(report.ReportTypeId, cancellationToken);

            reportEntity.Status = Enums.EnumReportStatusType.Processing;
            await _reportRepository.UpdateReportAsync(reportEntity, cancellationToken);

            var result = _contactGrpcService.GetContactReportByLocation(String.Empty);

            var excelModel = _mapper.Map<List<ContactExcelReportModel>>(result.Result.Data);

            string fileName = $"{reportTypePrm.ReportName}_{DateTime.Now.ToString("dd_MM_yyyy_HHmmss")}";
            string path = reportTypePrm.DestinationSavePath;
            byte[] filecontent = ExcelExportHelper.ExportExcel(excelModel, fileName, true);
            filecontent.SaveToExcel(path, fileName, out fileName);

            reportEntity.CompletedDate = DateTime.Now;
            reportEntity.FilePath = fileName;
            reportEntity.Status = Enums.EnumReportStatusType.Completed;
            await _reportRepository.UpdateReportAsync(reportEntity, cancellationToken);

            // Maybe we can add some publish event for notification service

            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}
