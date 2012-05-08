using System;
using UCDArch.Core.DomainModel;

namespace TPS.Core.Domain
{
    [Serializable]
    public class Subnet : DomainObjectWithTypedId<int>
    {
        public virtual string Name { get; set; }
    }
}