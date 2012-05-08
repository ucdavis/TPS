using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public sealed class DepartmentMap : FluentNHibernate.Mapping.ClassMap<Department>
    {
        public DepartmentMap()
        {
            Table("Departments");
            ReadOnly();

            Id(x => x.Id).GeneratedBy.Assigned();

            Map(x => x.Name);

            Cache.ReadOnly();
        }
    }
}