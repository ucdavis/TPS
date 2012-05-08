using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public sealed class DivisionMap : FluentNHibernate.Mapping.ClassMap<Division>
    {
        public DivisionMap()
        {
            Table("Divisions");
            ReadOnly();

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);

            Cache.ReadOnly();
        }
    }
}