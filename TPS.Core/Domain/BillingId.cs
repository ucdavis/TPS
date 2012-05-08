using System;
using UCDArch.Core.DomainModel;

namespace TPS.Core.Domain
{
    [Serializable]
    public class BillingId : DomainObjectWithTypedId<int>
    {
        public virtual string Name { get; set; }
    }
}