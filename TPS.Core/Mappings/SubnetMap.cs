using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public sealed class SubnetMap : FluentNHibernate.Mapping.ClassMap<Subnet>
    {
        public SubnetMap()
        {
            Table("Subnets");
            ReadOnly();

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);

            Cache.ReadOnly();
        }
    }
}