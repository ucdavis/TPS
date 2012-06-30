using System;
using UCDArch.Core.DomainModel;

namespace TPS.Core.Domain
{
    public class DepartmentFlattened : DomainObjectWithTypedId<string>
    {
        public virtual string Department { get; set; }

        public virtual string DepartmentNumber { get; set; }
    }
}