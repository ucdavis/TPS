using System;
using UCDArch.Core.DomainModel;

namespace TPS.Core.Domain
{
    [Serializable]
    public class Building : DomainObjectWithTypedId<int>
    {
        public virtual string Name { get; set; }

        public virtual string CaanZone { get; set; }
    }
}