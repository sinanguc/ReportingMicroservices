using Contact.Domain.Common;
using System;
using System.Collections.Generic;

namespace Contact.Domain.Entities
{
    public partial class Person : BaseEntity<Guid>, IEntity
    {
        public Person()
        {
            PersonContactInfo = new HashSet<PersonContactInfo>();
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public virtual ICollection<PersonContactInfo> PersonContactInfo { get; set; }

    }
}
