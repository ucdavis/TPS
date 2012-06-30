using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public class DepartmentFlattenedMap : FluentNHibernate.Mapping.ClassMap<DepartmentFlattened>
    {
        public DepartmentFlattenedMap()
        {
            Table("DepartmentsFlattened_v");
            ReadOnly();

            Id(x => x.Id).Column("DepartmentNumber").GeneratedBy.Assigned();

            Map(x => x.Department);

            Map(x => x.DepartmentNumber);

            Cache.ReadOnly();
        }
    }
}