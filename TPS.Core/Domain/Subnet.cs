using System;
using UCDArch.Core.DomainModel;

namespace TPS.Core.Domain
{
    [Serializable]
    public class Subnet : DomainObject
    {
        public virtual string Name { get; set; }
    }
}