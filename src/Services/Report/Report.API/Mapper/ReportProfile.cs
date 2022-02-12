using AutoMapper;
using Common.Dto.Report.Report;
using Contact.Grpc.Protos;
using EventBus.Messages.Events;
using Report.API.Model.Excel;

namespace Report.API.Mapper
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Entities.Report, InsertReportRequestDto>().ReverseMap();
            CreateMap<Entities.Report, ReportBackgroundServiceEvent>().ReverseMap();
            CreateMap<ContactReportDto, ContactExcelReportModel>().ReverseMap();
        }
    }
}
