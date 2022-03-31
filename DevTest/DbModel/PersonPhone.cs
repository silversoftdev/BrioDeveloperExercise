using System;
using System.Collections.Generic;

#nullable disable

namespace DevTest.DbModel
{
    public partial class PersonPhone
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int ExternalPersonId { get; set; }
        public string Phone { get; set; }

        public virtual Person ExternalPerson { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
