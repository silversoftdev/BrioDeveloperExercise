using System;
using System.Collections.Generic;

#nullable disable

namespace DevTest.DbModel
{
    public partial class Fkcode
    {
        public Fkcode()
        {
            ClaimFkcodes = new HashSet<ClaimFkcode>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int CodeType { get; set; }
        public int? Version { get; set; }

        public virtual ICollection<ClaimFkcode> ClaimFkcodes { get; set; }
    }
}
