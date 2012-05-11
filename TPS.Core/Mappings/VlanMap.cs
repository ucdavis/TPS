using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public class VlanMap : FluentNHibernate.Mapping.ClassMap<Vlan>
    {
        public VlanMap()
        {
            Table("Vlans");
            ReadOnly();

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);

            HasManyToMany(x => x.Contacts)
                .Table("VlanXTechContacts")
                .Cascade.None()
                .ParentKeyColumn("VlanId")
                .ChildKeyColumn("TechContactId");

            Cache.ReadOnly();
        }
    }
}