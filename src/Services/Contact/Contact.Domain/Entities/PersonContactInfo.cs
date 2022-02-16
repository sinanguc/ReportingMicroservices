using Contact.Domain.Common;
using Contact.Domain.Enums;
using System;

namespace Contact.Domain.Entities
{
    public partial class PersonContactInfo : BaseEntity<Guid>, IEntity
    {
        public Guid PersonId { get; set; }
        public ContactInfoType InfoType { get; set; }
        public string InfoDetail { get; set; }

        public virtual Person Person { get; set; }
    }
}
