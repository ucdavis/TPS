using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public class BuildingFlattenedMap : FluentNHibernate.Mapping.ClassMap<BuildingFlattened>
    {
        public BuildingFlattenedMap()
        {
            Table("BuildingsFlattened_v");
            ReadOnly();

            Id(x => x.Building).Column("Building").GeneratedBy.Assigned();

            Map(x => x.Building).Column("Building");

            Cache.ReadOnly();
        }
    }
}