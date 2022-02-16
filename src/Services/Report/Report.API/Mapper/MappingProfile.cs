using AutoMapper;
using Common.Dto.Report.Report;
using Common.Helpers.Pagination;
using Contact.Grpc.Protos;
using EventBus.Messages.Events;
using Report.API.Model.Excel;
using System.Collections.Generic;

namespace Report.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.Report, InsertReportRequestDto>().ReverseMap();
            CreateMap<Entities.Report, InsertReportResponseDto>().ReverseMap();
            CreateMap<Entities.Report, UpdateReportRequestDto>().ReverseMap();
            CreateMap<Entities.Report, ReportBackgroundServiceEvent>().ReverseMap();
            CreateMap<Entities.Report, ReportDto>().ReverseMap();
            CreateMap<PagedResult<Entities.Report>, PagedResult<ReportDto>>().ReverseMap();

            CreateMap<ContactReportDto, ContactExcelReportModel>().ReverseMap();
        }
    }
}
