using System;
using System.Collections.Generic;

#nullable disable

namespace DevTest.DbModel
{
    public partial class PersonRelated
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int ExternalPersonId { get; set; }
        public string RelatedFirstName { get; set; }
        public string RelatedMiddle { get; set; }
        public string RelatedLastName { get; set; }
        public DateTime? RelatedDateofBirth { get; set; }
        public string RelatedGender { get; set; }
        public int? Relationship { get; set; }

        public virtual Person ExternalPerson { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
