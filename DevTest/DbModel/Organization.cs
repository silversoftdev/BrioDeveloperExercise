using System;
using System.Collections.Generic;

#nullable disable

namespace DevTest.DbModel
{
    public partial class Organization
    {
        public Organization()
        {
            ClaimFkcodes = new HashSet<ClaimFkcode>();
            Claims = new HashSet<Claim>();
            People = new HashSet<Person>();
            PersonEmails = new HashSet<PersonEmail>();
            PersonPhones = new HashSet<PersonPhone>();
            PersonRelateds = new HashSet<PersonRelated>();
        }

        public int Id { get; set; }
        public string OrganizationId { get; set; }

        public virtual ICollection<ClaimFkcode> ClaimFkcodes { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<PersonEmail> PersonEmails { get; set; }
        public virtual ICollection<PersonPhone> PersonPhones { get; set; }
        public virtual ICollection<PersonRelated> PersonRelateds { get; set; }
    }
}
