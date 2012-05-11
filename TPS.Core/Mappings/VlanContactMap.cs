using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public class VlanContactMap : FluentNHibernate.Mapping.ClassMap<VlanContact>
    {
        public VlanContactMap()
        {
            Table("VLanContacts");
            ReadOnly();

            Id(x => x.Id).GeneratedBy.Identity();
            References(x => x.Subnet, "SubnetId").ForeignKey("Id");
            //Map(x => x.Subnet);
            References(x => x.Mask, "MaskId").ForeignKey("Id");
            //Map(x => x.Mask);
            References(x => x.Vlan, "VlanId").ForeignKey("Id");
            //Map(x => x.Vlan);
            //Map(x => x.Department);
            References(x => x.TechContact, "TechContactId").ForeignKey("Id");
            //Map(x => x.TechContact);
            //Map(x => x.Email);
            //Map(x => x.Phone);

            Cache.ReadOnly();
        }
    }
}