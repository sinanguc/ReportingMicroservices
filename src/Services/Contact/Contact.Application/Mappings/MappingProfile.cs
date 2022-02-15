using AutoMapper;
using Common.Dto.Contact;
using Common.Helpers.Pagination;
using Contact.Application.Features.Contacts.Commands.InsertContact;
using Contact.Application.Features.Contacts.Queries.GetContactReportByLocation;
using Contact.Application.Features.Contacts.Queries.GetContactsList;
using Contact.Domain.Entities;
using System.Collections.Generic;

namespace Contact.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PagedResult<ContactDto>, PagedResult<ContactVm>>().ReverseMap();
            CreateMap<ContactDto, ContactVm>().ReverseMap();

            CreateMap<InsertContactRequestDto, Person>().ReverseMap();
            CreateMap<InsertContactResponseDto, Person>().ReverseMap();
            CreateMap<UpdateContactRequestDto, Person>().ReverseMap();
            CreateMap<UpdateContactResponseDto, Person>().ReverseMap();

            CreateMap<ContactInfoDto, ContactDetailVm>().ReverseMap();
            CreateMap<ContactInfoDto.WithId, ContactDetailVm>().ReverseMap();
           

            CreateMap<InsertContactInfoRequestDto, PersonContactInfo>().ReverseMap();
            CreateMap<InsertContactInfoResponseDto, PersonContactInfo>().ReverseMap();
            CreateMap<UpdateContactInfoResponseDto, PersonContactInfo>().ReverseMap();

            CreateMap<ContactInfoDto, PersonContactInfo>().ReverseMap();
            CreateMap<ContactInfoDto.WithId, PersonContactInfo>().ReverseMap();
            CreateMap<PagedResult<ContactInfoDto>, PagedResult<PersonContactInfo>>().ReverseMap();

            CreateMap<ContactReportByLocationDto, ContactReportByLocationVm>().ReverseMap();
        }
    }
}
