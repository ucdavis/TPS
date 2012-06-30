using System;
using System.Collections.Generic;
using UCDArch.Core.DomainModel;

namespace TPS.Core.Domain
{
    public class VlanFlattened : DomainObjectWithTypedId<string>
    {
        public virtual string Vlan { get; set; }
    }
}