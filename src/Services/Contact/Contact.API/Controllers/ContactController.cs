using Common.Dto.Contact;
using Common.Dto.Contact.Filters;
using Common.Dto.Shared;
using Common.Messages;
using Contact.Application.Features.Contacts.Commands.DeleteContact;
using Contact.Application.Features.Contacts.Commands.DeleteContactInfo;
using Contact.Application.Features.Contacts.Commands.InsertContact;
using Contact.Application.Features.Contacts.Commands.InsertContactInfo;
using Contact.Application.Features.Contacts.Commands.UpdateContact;
using Contact.Application.Features.Contacts.Commands.UpdateContactInfo;
using Contact.Application.Features.Contacts.Queries.GetContactInfosList;
using Contact.Application.Features.Contacts.Queries.GetContactsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("GetContact")]
        [ProducesResponseType(typeof(GenericResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GenericResult>> GetContacts([FromQuery] ContactFilter filter)
        {
            var query = new GetContactsListQuery(filter);
            var contacts = await _mediator.Send(query);

            GenericResult result = new GenericResult();
            result.Data = contacts;
            result.Message = GenericMessages.Successfully_Listed;
            return Ok(result);
        }

        [HttpPost("AddContact")]
        [ProducesResponseType(typeof(GenericResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GenericResult>> AddContact([FromBody] InsertContactRequestDto request)
        {
            var command = new InsertContactCommand(request);
            var person = await _mediator.Send(command);

            GenericResult result = new GenericResult();
            result.Data = person;
            result.Message = GenericMessages.Successfully_Registered;

            return Ok(result);
        }

        [HttpPut("EditContact")]
        [ProducesResponseType(typeof(GenericResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GenericResult>> EditContact([FromBody] UpdateContactRequestDto request)
        {
            var command = new UpdateContactCommand(request);
            var person = await _mediator.Send(command);

            GenericResult result = new GenericResult();
            result.Data = person;
            result.Message = GenericMessages.Successfully_Registered;

            return Ok(result);
        }

        [HttpDelete("DeleteContact")]
        [ProducesResponseType(typeof(GenericResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GenericResult>> DeleteContact([FromBody] DeleteContactRequestDto request)
        {
            var command = new DeleteContactCommand(request.Id);
            var person = await _mediator.Send(command);

            GenericResult result = new GenericResult();
            result.Data = person;
            result.Message = GenericMessages.Successfully_Deleted_Record;

            return Ok(result);
        }


        [HttpGet("GetContactInfo")]
        [ProducesResponseType(typeof(GenericResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GenericResult>> GetContactInfos([FromQuery] ContactInfoFilter filter)
        {
            var query = new GetContactInfosListQuery(filter);
            var contactInfos = await _mediator.Send(query);

            GenericResult result = new GenericResult();
            result.Data = contactInfos;
            result.Message = GenericMessages.Successfully_Listed;
            return Ok(result);
        }

        [HttpDelete("AddContactInfo")]
        [ProducesResponseType(typeof(GenericResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GenericResult>> AddContactInfo([FromBody] InsertContactInfoRequestDto request)
        {
            var command = new InsertContactInfoCommand(request);
            var personConcactInfo = await _mediator.Send(command);

            GenericResult result = new GenericResult();
            result.Data = personConcactInfo;
            result.Message = GenericMessages.Successfully_Deleted_Record;

            return Ok(result);
        }

        [HttpDelete("UpdateContactInfo")]
        [ProducesResponseType(typeof(GenericResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GenericResult>> UpdateContactInfo([FromBody] UpdateContactInfoRequestDto request)
        {
            var command = new UpdateContactInfoCommand(request);
            var personConcactInfo = await _mediator.Send(command);

            GenericResult result = new GenericResult();
            result.Data = personConcactInfo;
            result.Message = GenericMessages.Successfully_Deleted_Record;

            return Ok(result);
        }

        [HttpDelete("DeleteContactInfo")]
        [ProducesResponseType(typeof(GenericResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GenericResult>> DeleteContactInfo([FromBody] DeleteContactInfoRequestDto request)
        {
            var command = new DeleteContactInfoCommand(request.Id);
            var personConcactInfoId = await _mediator.Send(command);

            GenericResult result = new GenericResult();
            result.Data = personConcactInfoId;
            result.Message = GenericMessages.Successfully_Deleted_Record;

            return Ok(result);
        }

    }
}
