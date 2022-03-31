using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevTest.ViewModels.Dtos
{
    public class PersonDetailDtoModal
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string ExternalPersonId { get; set; }
        public string FirstName { get; set; }
        public string Middle { get; set; }
        public string LastName { get; set; }
        public List<PersonPhoneDto> phones { get; set; }
        public List<PersonClaimsDto> claims { get; set; }
    }
    public class PersonPhoneDto
    {
        public string phone { get; set; }

    }
    public class PersonClaimsDto
    {
        public int ExternalPersonId { get; set; }
        public string ExternalClaimId { get; set; }
        public int? ServiceNumber { get; set; }
        public string ServiceName { get; set; }
        public string ServiceAddress { get; set; }
        public string ServiceCity { get; set; }
        public string ServiceState { get; set; }
        public string ServiceZip { get; set; }
        public string ServiceZip2 { get; set; }
    }
}
