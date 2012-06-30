using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public class VlanFlattenedMap : FluentNHibernate.Mapping.ClassMap<VlanFlattened>
    {
        public VlanFlattenedMap()
        {
            Table("VlanFlattened_v");
            ReadOnly();

            Id(x => x.Id).Column("Vlan").Column("Vlan").GeneratedBy.Assigned();

            Map(x => x.Vlan);

            Cache.ReadOnly();
        }
    }
}