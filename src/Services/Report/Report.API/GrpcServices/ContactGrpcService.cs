using System;
using System.Threading.Tasks;
using Contact.Grpc.Protos;

namespace Report.API.GrpcServices
{
    public class ContactGrpcService
    {
        private readonly ContactProtoService.ContactProtoServiceClient _contactProtoService;

        public ContactGrpcService(ContactProtoService.ContactProtoServiceClient contactProtoServiceClient)
        {
            _contactProtoService = contactProtoServiceClient ?? throw new ArgumentNullException(nameof(contactProtoServiceClient));
        }

        public async Task<ContactReportByLocationResponse> GetContactReportByLocation(string locationName)
        {
            ContactReportByLocationRequest request = new()
            {
                LocationName = locationName
            };
            return await _contactProtoService.GetContactReportByLocationAsync(request);
        }
    }
}
