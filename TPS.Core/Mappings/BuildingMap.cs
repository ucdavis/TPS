using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public sealed class BuildingMap : FluentNHibernate.Mapping.ClassMap<Building>
    {
        public BuildingMap()
        {
            Table("Buildings");
            ReadOnly();

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);
            Map(x => x.CaanZone);

            Cache.ReadOnly();
        }
    }
}