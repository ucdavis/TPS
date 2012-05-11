using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public class CollegeMap : FluentNHibernate.Mapping.ClassMap<College>
    {
        public CollegeMap()
        {
            Table("Colleges");
            ReadOnly();

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);

            Cache.ReadOnly();
        }
    }
}