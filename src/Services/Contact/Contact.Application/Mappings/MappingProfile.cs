using AutoMapper;
using Common.Dto.Contact;
using Common.Helpers.Pagination;
using Contact.Application.Features.Contacts.Commands.InsertContact;
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

            CreateMap<ContactDetailDto, ContactDetailVm>().ReverseMap();
            CreateMap<ContactDetailDto.WithId, ContactDetailVm>().ReverseMap();
        }
    }
}
