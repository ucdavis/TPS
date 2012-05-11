using TPS.Core.Domain;

namespace TPS.Core.Mappings
{
    public class DataNamMap : FluentNHibernate.Mapping.ClassMap<DataNam>
    {
        public DataNamMap()
        {
            Table("DataNams3");
            ReadOnly();

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.NamNumber);
            Map(x => x.Area);
            References(x => x.Building, "BuildingId").ForeignKey("Id");
            Map(x => x.Room);
            References(x => x.Department, "DepartmentId").ForeignKey("Id");
            References(x => x.BillingId, "BillingId").ForeignKey("Id");
            References(x => x.Vlan, "VlanId").ForeignKey("Id");
            References(x => x.Division, "DivisionId").ForeignKey("Id");
            References(x => x.College, "CollegeId").ForeignKey("Id");
            References(x => x.Status, "StatusId").ForeignKey("Id");
            Map(x => x.PortNumber);

            Cache.ReadOnly();
        }
    }
}