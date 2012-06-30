using System;
using UCDArch.Core.DomainModel;

namespace TPS.Core.Domain
{
    [Serializable]
    public class Department : DomainObjectWithTypedId<string>
    {
        public virtual string Name { get; set; }
    }
}