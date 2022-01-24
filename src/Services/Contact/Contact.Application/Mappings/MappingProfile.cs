using AutoMapper;
using Common.Dto.Contact;
using Common.Helpers.Pagination;
using Contact.Application.Features.Contacts.Commands.InsertContact;
using Contact.Application.Features.Contacts.Queries.GetContactsList;
using Contact.Domain.Entities;

namespace Contact.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PagedResult<ContactDto>, PagedResult<ContactVm>>().ReverseMap();
            CreateMap<ContactDto, ContactVm>().ReverseMap();
            CreateMap<InsertContactDto, Person>().ReverseMap();
            CreateMap<InsertContactCommand, Person>().ReverseMap();
        }
    }
}
