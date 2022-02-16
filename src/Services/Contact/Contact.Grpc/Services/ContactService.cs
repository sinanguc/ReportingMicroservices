using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contact.Application.Features.Contacts.Queries.GetContactReportByLocation;
using Contact.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Contact.Grpc.Services
{
    public class ContactService : ContactProtoService.ContactProtoServiceBase
    {
        private readonly IMediator _mediator;

        public ContactService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public override async Task<ContactReportByLocationResponse> GetContactReportByLocation(ContactReportByLocationRequest request, ServerCallContext context)
        {
            var query = new GetContactReportByLocationForExportFileQuery(request.LocationName);
            var contacts = await _mediator.Send(query);

            var list = new List<ContactReportDto>();
            contacts.ForEach(d => list.Add(
                new ContactReportDto()
                {
                    LocationName = d.LocationName,
                    PersonCountInLocation = d.PersonCountInLocation,
                    PhoneNumberCountInLocation = d.PhoneNumberCountInLocation
                }));

            var response = new ContactReportByLocationResponse()
            {
                Data = {list}
            };

            return await Task.FromResult(response);
        }
    }
}
