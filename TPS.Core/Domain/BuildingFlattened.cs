using System;
using UCDArch.Core.DomainModel;

namespace TPS.Core.Domain
{
    public class BuildingFlattened : DomainObjectWithTypedId<string>
    {
        public virtual string Building { get; set; }
    }
}