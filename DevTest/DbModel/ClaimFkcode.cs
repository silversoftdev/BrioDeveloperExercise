using System;
using System.Collections.Generic;

#nullable disable

namespace DevTest.DbModel
{
    public partial class ClaimFkcode
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int ExternalPersonId { get; set; }
        public int ExternalClaimId { get; set; }
        public int FkcodeId { get; set; }

        public virtual Claim ExternalClaim { get; set; }
        public virtual Person ExternalPerson { get; set; }
        public virtual Fkcode Fkcode { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
