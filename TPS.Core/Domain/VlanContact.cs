using System;
using UCDArch.Core.DomainModel;

namespace TPS.Core.Domain
{
    [Serializable]
    public class VlanContact : DomainObjectWithTypedId<int>
    {
        public virtual Subnet Subnet { get; set; }

        public virtual Mask Mask { get; set; }

        public virtual Vlan Vlan { get; set; }

        //public virtual string Department { get; set; }

        public virtual TechContact TechContact { get; set; }

        //public virtual string Email { get; set; }

        //public virtual string Phone { get; set; }
    }
}