using System;
using UCDArch.Core.DomainModel;

namespace TPS.Core.Domain
{
    public class DataNamFlattened : DomainObject
    {
        public virtual string NamNumber { get; set; }

        //public virtual string Area { get; set; }

        public virtual string Building { get; set; }

        public virtual string Room { get; set; }

        public virtual string Department { get; set; }

        //public virtual string CaanZone { get; set; }

        public virtual string BillingId { get; set; }

        public virtual string Vlan { get; set; }

        //public virtual string Division { get; set; }

        //public virtual string College { get; set; }

        public virtual string Status { get; set; }

        public virtual string PortNumber { get; set; }
    }
}