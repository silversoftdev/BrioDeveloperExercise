using System;
using System.Collections.Generic;

#nullable disable

namespace DevTest.DbModel
{
    public partial class Person
    {
        public Person()
        {
            ClaimFkcodes = new HashSet<ClaimFkcode>();
            Claims = new HashSet<Claim>();
            PersonEmails = new HashSet<PersonEmail>();
            PersonPhones = new HashSet<PersonPhone>();
            PersonRelateds = new HashSet<PersonRelated>();
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string ExternalPersonId { get; set; }
        public string SubscriberNumber { get; set; }
        public string SubscriberId { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Suffix { get; set; }
        public string FirstName { get; set; }
        public string Middle { get; set; }
        public string LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string PostalCode2 { get; set; }
        public int? BillingNumber { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingPostalCode { get; set; }
        public string BillingPostalCode2 { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual ICollection<ClaimFkcode> ClaimFkcodes { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
        public virtual ICollection<PersonEmail> PersonEmails { get; set; }
        public virtual ICollection<PersonPhone> PersonPhones { get; set; }
        public virtual ICollection<PersonRelated> PersonRelateds { get; set; }
    }
}
