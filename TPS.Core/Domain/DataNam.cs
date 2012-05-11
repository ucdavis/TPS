using System;
using UCDArch.Core.DomainModel;

namespace TPS.Core.Domain
{
    [Serializable]
    public class DataNam : DomainObject
    {
        public virtual string NamNumber { get; set; }

        public virtual string Area { get; set; }

        public virtual Building Building { get; set; }

        public virtual string Room { get; set; }

        public virtual Department Department { get; set; }

        public virtual BillingId BillingId { get; set; }

        public virtual Vlan Vlan { get; set; }

        public virtual Division Division { get; set; }

        public virtual College College { get; set; }

        public virtual StatusCode Status { get; set; }

        public virtual string PortNumber { get; set; }
    }
}