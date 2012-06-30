using System;
using UCDArch.Core.DomainModel;

namespace TPS.Core.Domain
{
    public class Building : DomainObject
    {
        public virtual string Name { get; set; }

        public virtual string CaanZone { get; set; }
    }
}