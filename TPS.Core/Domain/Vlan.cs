using System;
using System.Collections.Generic;
using UCDArch.Core.DomainModel;

namespace TPS.Core.Domain
{
    [Serializable]
    public class Vlan : DomainObjectWithTypedId<int>
    {
        public virtual string Name { get; set; }

        public virtual IList<TechContact> Contacts { get; set; }
    }
}